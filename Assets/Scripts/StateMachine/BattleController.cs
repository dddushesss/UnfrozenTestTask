namespace StateMachine
{
    public class BattleController : StateMachine
    {
        private BattleInterfaceController _interfaceControllers;

        public BattleInterfaceController InterfaceControllers => _interfaceControllers;

        public void SetInterface(BattleInterfaceController interfaceController)
        {
            _interfaceControllers = interfaceController;
        }
    }
}