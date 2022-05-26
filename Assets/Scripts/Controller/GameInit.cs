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
            CharacterFactory characterFactory = new CharacterFactory(mapView, data.CharactersData);
            characterFactory.Spawn();

            BattleController battleController = new BattleController();
            var battleInterface = new BattleInterfaceController(data.BattleInterfaceData);

            battleController.SetInterface(battleInterface.Spawn());
            battleController.SetLists(characterFactory.EnemyCharacters, characterFactory.PlayerCharacters)
                ;
            battleController.Initialize(new PlayerTurnState(
                characterFactory.PlayerCharacters[Random.Range(0, characterFactory.PlayerCharacters.Count)],
                battleController));
        }
    }
}