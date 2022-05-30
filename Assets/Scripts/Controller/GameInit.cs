using Characters;
using Map;
using Singleton;
using StateMachine;
using StateMachine.States;
using UnityEngine;

namespace Controller
{
    internal sealed class GameInit
    {
        public GameInit(Controllers controllers, Data.Data data, MapView mapView)
        {
            Singleton<TimerHelper>.Init("TimerHelper");
            controllers.Add(Singleton<SingletonManager>.Instance);
            
            CharacterFactory characterFactory = new CharacterFactory(mapView, data.CharactersData);
            characterFactory.Spawn();

            
            var battleInterface = new BattleInterfaceController(data.BattleInterfaceData);
            battleInterface.Spawn();
            BattleController battleController = new BattleController(battleInterface, characterFactory.EnemyCharacters, characterFactory.PlayerCharacters);
          
            
            battleController.Initialize();
        }
    }
}