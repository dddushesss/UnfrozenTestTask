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
            controller.InterfaceControllers.OnSkipButtonPressed += Skip;
            controller.InterfaceControllers.SetButtonsEnabled(true);
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

        private void Skip()
        {
            controller.InterfaceControllers.OnAttackButtonPressed -= ChoseCharacterToAttack;
            controller.InterfaceControllers.OnSkipButtonPressed -= Skip;
            stateMachine.ChangeState(
                new EnemyTurnState(controller.Enemies[Random.Range(0, controller.Enemies.Count)],
                    stateMachine));
            controller.Enemies.ForEach(enemy =>
            {
                enemy.View.CanChoose(false);
                enemy.View.OnCharacterChosen -= Attack;
            });
        }
        private void Attack(Character target)
        {
            controller.InterfaceControllers.OnAttackButtonPressed -= ChoseCharacterToAttack;
            controller.InterfaceControllers.OnSkipButtonPressed -= Skip;
            controller.Enemies.ForEach(enemy =>
            {
                enemy.View.CanChoose(false);
                enemy.View.OnCharacterChosen -= Attack;
            });
            if (!target.IsInFront)
            {
                target.SwitchPlaces(controller.Enemies.First(c => c.IsInFront), () => { PerformAttack(target); });
                return;
            }

            PerformAttack(target);
        }

        private void PerformAttack(Character target)
        {
            character.Attack(target);
            Singleton<TimerHelper>.Instance.StartTimer(
                () =>
                {
                    stateMachine.ChangeState(
                        new EnemyTurnState(controller.Enemies[Random.Range(0, controller.Enemies.Count)],
                            stateMachine));
                }, 1f);
        }
    }
}