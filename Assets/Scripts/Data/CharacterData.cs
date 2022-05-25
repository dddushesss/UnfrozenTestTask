using UnityEngine;

namespace Data
{

    [CreateAssetMenu(fileName = "CharactersData", menuName = "Data/CharacterData", order = 0)]
    public class CharacterData : ScriptableObject
    {
        [SerializeField] private int dmg;
        [SerializeField] private GameObject prefab;

        public int Dmg => dmg;

        public GameObject Prefab => prefab;
    }
}