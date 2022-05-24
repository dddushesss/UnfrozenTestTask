

using Singleton;

namespace Controller
{
    internal sealed class GameInit
    {
        public GameInit(Controllers controllers, Data.Data data)
        {
            Singleton<TimerHelper>.Init("TimerHelper");
            
        }

    }
}