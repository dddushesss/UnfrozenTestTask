using System.Collections.Generic;
using Characters;

namespace StateMachine
{
    public class BattleController : StateMachine
    {
        private BattleInterfaceController _interfaceControllers;
        private List<Character> _enemies;
        private List<Character> _player;

        public BattleInterfaceController InterfaceControllers => _interfaceControllers;

        public List<Character> Enemies => _enemies;

        public List<Character> Player => _player;

        public void SetInterface(BattleInterfaceController interfaceController)
        {
            _interfaceControllers = interfaceController;
        }

        public void SetLists(List<Character> enemies, List<Character> player)
        {
            _enemies = new List<Character>();
            _player = new List<Character>();
            _enemies.AddRange(enemies);
            _player.AddRange(player);
        }
    }
}