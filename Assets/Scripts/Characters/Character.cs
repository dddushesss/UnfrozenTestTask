using Data;
using Map;
using Spine.Unity;
using UnityEngine;

namespace Characters
{
    public class Character
    {
        private CharacterView _view;
        private CharacterData _data;
        private int _hp;
        private Team _team;

        public Character(CharacterData data)
        {
            _data = data;
        }

        public void Spawn(Vector3 pos, Team team)
        {
            var data = _data.SkeletonData;
            data.GetSkeletonData(false);

            var go = SkeletonAnimation.NewSkeletonAnimationGameObject(data);
            _view = go.gameObject.GetOrAddComponent<CharacterView>();
            _view.name = _data.InitSkin;
            _view.transform.SetPositionAndRotation(pos, Quaternion.identity);
            _team = team;
            _view.SetUp(_data, _team is Team.Enemy);
            _hp = _data.Hp;
            go.Initialize(false);
        }

        public void GetDamage(int damage)
        {
            if (_hp - damage <= 0)
            {
                Death();
                return;
            }
            _hp -= damage;
            _view.PlayAnimation(_data.GetHitAnimation);
        }

        private void Death()
        {
            Debug.Log("Death");
        }
    }
}