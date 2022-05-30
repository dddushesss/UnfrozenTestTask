using System.Collections.Generic;
using System.Linq;
using Characters;
using Singleton;
using UnityEngine;

namespace StateMachine.States
{
    public class PlayerTurnState : State
    {
        private BattleController controller;
        
        public PlayerTurnState(Character character, StateMachine stateMachine, List<Character> characters) : base(character, stateMachine, characters)
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
            controller.NextTurn();
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
                () => { controller.NextTurn(); }, 1f);
        }
    }
}