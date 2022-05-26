using System.IO;

using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public class Data : ScriptableObject
    {
        

        [SerializeField] private bool isUsingNonDefaultCharacterListData;
        [SerializeField] private string _charactersDataPath = "CharactersList";
        private CharactersListData _charactersData;
        
        [SerializeField] private bool isUsingNonDefaultBattleInterfaceData;
        [SerializeField] private string battleInterfaceDataPath = "BattleInterfaceData";
        private BattleInterfaceData _battleInterfaceData;

        [Space, SerializeField] private string pathToLevel = "Data/";
        [SerializeField] private string pathToData = "Data/";

        
        public BattleInterfaceData BattleInterfaceData
        {
            get
            {
                if (_battleInterfaceData == null)
                {
                    _battleInterfaceData = Load<BattleInterfaceData>(battleInterfaceDataPath, isUsingNonDefaultBattleInterfaceData);
                }
        
                return _battleInterfaceData;
            }
        }

        public CharactersListData CharactersData
        {
            get
            {
                if (_charactersData == null)
                {
                    _charactersData = Load<CharactersListData>(_charactersDataPath, isUsingNonDefaultCharacterListData);
                }
        
                return _charactersData;
            }
        }
        
     

        private T Load<T>(string resourcesPath, bool isNonDefaultPath) where T : Object
        {
            return Resources.Load<T>(
                Path.ChangeExtension(isNonDefaultPath ? pathToLevel + resourcesPath : pathToData + resourcesPath,
                    null));
        }
    }
}