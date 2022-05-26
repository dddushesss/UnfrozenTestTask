using Characters;

namespace StateMachine.States
{
    public class PlayerTurnState : State
    {
        public PlayerTurnState(Character character, StateMachine stateMachine) : base(character, stateMachine)
        {
        }

        public override void Enter()
        {
            base.Enter();
            ((BattleController)stateMachine).InterfaceControllers.OnAttackButtonPressed += Attack;
        }

        private void Attack()
        {
            
        }
    }
}