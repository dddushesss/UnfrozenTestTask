using System.Collections.Generic;
using Data;
using Map;
using UnityEngine;

namespace Characters
{
    public class CharacterFactory
    {
        private MapView _mapView;
        private CharactersListData _data;
        private List<Character> _characters;

        public CharacterFactory(MapView mapView, CharactersListData data)
        {
            _mapView = mapView;
            _data = data;
            _characters = new List<Character>();
        }

        public void Spawn()
        {
            foreach (var mapViewSpawnPos in _mapView.Spawners)
            {
                var charData = _data.CharacterDataList[Random.Range(0, _data.CharacterDataList.Count)];
                var character = new Character(charData);
                character.Spawn(mapViewSpawnPos.SpawnPos, mapViewSpawnPos.Team);
            }
        }
    }
}