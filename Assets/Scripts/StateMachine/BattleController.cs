using System;
using System.Collections.Generic;
using Characters;
using DG.Tweening;
using StateMachine.States;
using Random = UnityEngine.Random;

namespace StateMachine
{
    public class BattleController : StateMachine
    {
        private BattleInterfaceController _interfaceControllers;
        private List<Character> _enemies;
        private List<Character> _player;
        private PlayerTurnState _playerTurn;
        private EnemyTurnState _enemyTurn;
        private Queue<State> _stateQueue;
        private HitView _hitView;
        
        public event Action OnCanHit;

        public BattleInterfaceController InterfaceControllers => _interfaceControllers;

        public List<Character> Enemies => _enemies;

        public List<Character> Player => _player;

        public BattleController(BattleInterfaceController interfaceControllers, IEnumerable<Character> enemies,
            IEnumerable<Character> player, HitView hitView)
        {
            _interfaceControllers = interfaceControllers;

            _enemies = new List<Character>();
            _player = new List<Character>();
            _enemies.AddRange(enemies);
            _player.AddRange(player);
            _stateQueue = new Queue<State>(2);
            _playerTurn = new PlayerTurnState(_player[Random.Range(0, _player.Count)], this, _player);
            _enemyTurn = new EnemyTurnState(_enemies[Random.Range(0, _player.Count)], this, _enemies);
            _hitView = hitView;
        }


        public void Initialize()
        {
            base.Initialize(_playerTurn);
            _stateQueue.Enqueue(_enemyTurn);
            _stateQueue.Enqueue(_playerTurn);
        }

        public void NextTurn()
        {
            var curTurn = _stateQueue.Dequeue();
            _stateQueue.Enqueue(curTurn);
            curTurn.SetNextCharacter();
            ChangeState(curTurn);
        }

        public void ShowHit(Character player, Character enemy)
        {
            _hitView.gameObject.SetActive(true);
            player.View.Renderer.sortingOrder = 5;
            enemy.View.Renderer.sortingOrder = 5;
            player.View.transform.DOMove(_hitView.PlayerPos.SpawnPos, 1f);
            enemy.View.transform.DOMove(_hitView.EnemyPos.SpawnPos, 1f).OnComplete(() => OnCanHit?.Invoke());
        }

        public void ReturnCharactersOnDefaultPosition(Character player, Character enemy)
        {
            OnCanHit = null;
            _hitView.gameObject.SetActive(false);
            player.View.Renderer.sortingOrder = 3;
            enemy.View.Renderer.sortingOrder = 3;
            player.View.transform.DOMove(player.DefaultPosition, 1f);
            enemy.View.transform.DOMove(enemy.DefaultPosition, 1f).OnComplete(NextTurn);
           
        }
        
    }
}