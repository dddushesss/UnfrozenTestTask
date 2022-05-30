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

        public EnemyTurnState(Character character, StateMachine stateMachine, List<Character> characters) : base(character, stateMachine, characters)
        {
            controller = (BattleController)stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            controller.InterfaceControllers.SetButtonsEnabled(false);
            Singleton<TimerHelper>.Instance.StartTimer(() =>
            {
                if (!character.IsInFront)
                    character.SwitchPlaces(controller.Enemies.First(c => c.IsInFront), null);
                Singleton<TimerHelper>.Instance.StartTimer(() =>
                {
                    var target = controller.Player[Random.Range(0, controller.Player.Count)];
                    if (!target.IsInFront)
                        target.SwitchPlaces(controller.Player.First(c => c.IsInFront),
                            () => { character.Attack(target); });
                    else
                    {
                        character.Attack(target);
                    }

                    Singleton<TimerHelper>.Instance.StartTimer(
                        () => { controller.NextTurn(); }, 1f);
                }, 0.3f);
            }, 0.3f);
        }
    }
}