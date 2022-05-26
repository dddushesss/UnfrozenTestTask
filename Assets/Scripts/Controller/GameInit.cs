

using Characters;
using Map;
using Singleton;

namespace Controller
{
    internal sealed class GameInit
    {
        public GameInit(Controllers controllers, Data.Data data, MapView mapView)
        {
            Singleton<TimerHelper>.Init("TimerHelper");
            CharacterFactory characterFactory = new CharacterFactory(mapView, data.CharactersData);
            characterFactory.Spawn();
        }

    }
}