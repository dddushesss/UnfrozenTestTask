using System.Collections.Generic;
using System.Linq;
using Characters;
using Singleton;
using UnityEngine;

namespace StateMachine.States
{
    public class EnemyTurnState : State
    {
        private BattleController controller;

        public EnemyTurnState(Character character, StateMachine stateMachine, List<Character> characters) : base(
            character, stateMachine, characters)
        {
            controller = (BattleController)stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            controller.InterfaceControllers.SetButtonsEnabled(false);


            Singleton<TimerHelper>.Instance.StartTimer(() =>
            {
                var target = controller.Player[Random.Range(0, controller.Player.Count)];
                controller.OnCanHit += () => character.Attack(target);
                controller.ShowHit(target, character);
                character.View.OnAnimationFinished += () =>
                    controller.ReturnCharactersOnDefaultPosition(target, character);
            }, 1f);
        }
    }
}