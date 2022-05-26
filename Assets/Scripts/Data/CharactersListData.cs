using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [CreateAssetMenu(fileName = "CharactersList", menuName = "Data/CharacterList", order = 0)]
    public class CharactersListData : ScriptableObject
    {
        [SerializeField] private List<CharacterData> _characterDataList;

        public List<CharacterData> CharacterDataList => _characterDataList;
    }
}