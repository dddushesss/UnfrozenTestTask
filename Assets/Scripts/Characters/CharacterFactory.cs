using System.Collections.Generic;
using Data;

namespace Characters
{
    public class CharacterFactory
    {
        private MapView _mapView;
        private CharactersListData _data;

        public CharacterFactory(MapView mapView, CharactersListData data)
        {
            _mapView = mapView;
            _data = data;
        }
    }
}