using Data;
using UnityEngine;

namespace Characters
{
    public class Character
    {
        private CharacterView _view;
        private CharacterData _data;

        public Character(CharacterData data)
        {
            _data = data;
        }

        public void Spawn(Vector3 pos)
        {
            var go = Object.Instantiate(_data.Prefab, pos, Quaternion.identity);
            _view = go.GetOrAddComponent<CharacterView>();
            _view.SetUp(_data);
        }
    }
}