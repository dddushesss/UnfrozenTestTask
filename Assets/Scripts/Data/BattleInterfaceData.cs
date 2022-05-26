using UnityEngine;
using UnityEngine.UI;

namespace Data
{
    [CreateAssetMenu(fileName = "BattleInterfaceData", menuName = "Data/BattleInterfaceData", order = 0)]
    public class BattleInterfaceData : ScriptableObject
    {
        [SerializeField] private BattleInterfaceControllerView view;

        public BattleInterfaceControllerView View => view;
    }
}