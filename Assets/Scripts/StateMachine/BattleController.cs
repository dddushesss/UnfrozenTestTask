using System.Collections.Generic;
using Characters;
using StateMachine.States;
using UnityEngine;

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

        public BattleInterfaceController InterfaceControllers => _interfaceControllers;

        public List<Character> Enemies => _enemies;

        public List<Character> Player => _player;

        public BattleController(BattleInterfaceController interfaceControllers, IEnumerable<Character> enemies,
            IEnumerable<Character> player)
        {
            _interfaceControllers = interfaceControllers;

            _enemies = new List<Character>();
            _player = new List<Character>();
            _enemies.AddRange(enemies);
            _player.AddRange(player);
            _stateQueue = new Queue<State>(2);
            _playerTurn = new PlayerTurnState(_player[Random.Range(0, _player.Count)], this, _player);
            _enemyTurn = new EnemyTurnState(_enemies[Random.Range(0, _player.Count)], this, _enemies);
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
    }
}