using System.IO;

using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "Data", menuName = "Data/Data")]
    public class Data : ScriptableObject
    {
        

        [SerializeField] private bool isUsingNonDefaultFlowfieldPathfindingData;
        [SerializeField] private string _charactersDataPath = "CharactersList";
        private CharactersListData _charactersData;

        [Space, SerializeField] private string pathToLevel = "Data/";
        [SerializeField] private string pathToData = "Data/";


        public CharactersListData CharactersData
        {
            get
            {
                if (_charactersData == null)
                {
                    _charactersData = Load<CharactersListData>(_charactersDataPath, isUsingNonDefaultFlowfieldPathfindingData);
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