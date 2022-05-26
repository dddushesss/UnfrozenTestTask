using System.Linq;
using Characters;
using Singleton;
using UnityEngine;

namespace StateMachine.States
{
    public class PlayerTurnState : State
    {
        private BattleController controller;

        public PlayerTurnState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            controller = (BattleController)stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            controller.InterfaceControllers.OnAttackButtonPressed += ChoseCharacterToAttack;
            if (!character.IsInFront)
                character.SwitchPlaces(controller.Player.First(c => c.IsInFront), null);
        }

        private void ChoseCharacterToAttack()
        {
            controller.Enemies.ForEach(enemy =>
            {
                enemy.View.CanChoose(true);
                enemy.View.OnCharacterChosen += Attack;
            });
        }

        private void Attack(Character target)
        {
            controller.Enemies.ForEach(enemy =>
            {
                enemy.View.CanChoose(false);
                enemy.View.OnCharacterChosen -= Attack;
            });
            if (!target.IsInFront)
                target.SwitchPlaces(controller.Enemies.First(c => c.IsInFront), () =>
                {
                    character.Attack(target);
                    Singleton<TimerHelper>.Instance.StartTimer(
                        () =>
                        {
                            stateMachine.ChangeState(
                                new EnemyTurnState(controller.Enemies[Random.Range(0, controller.Enemies.Count)],
                                    stateMachine));
                        }, 1f);
                });
        }
    }
}