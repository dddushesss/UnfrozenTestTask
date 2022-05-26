using System.Linq;
using Characters;
using Singleton;

namespace StateMachine.States
{
    public class EnemyTurnState : State
    {
        private BattleController controller;

        public EnemyTurnState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
            controller = (BattleController)stateMachine;
        }

        public override void Enter()
        {
            base.Enter();
            Singleton<TimerHelper>.Instance.StartTimer(() =>
            {
                if (!character.IsInFront)
                    character.SwitchPlaces(controller.Enemies.First(c => c.IsInFront), null);
                Singleton<TimerHelper>.Instance.StartTimer(() =>
                {
                    
                }, 0.3f);
            }, 0.3f);
        }
    }
}