using System;
using Data;
using DG.Tweening;
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
        private bool isInFront;

        public bool IsPlayer => _team is Team.Player;
        public CharacterView View => _view;

        public bool IsInFront => isInFront;

        public Character(CharacterData data)
        {
            _data = data;
        }

        public void Spawn(SpawnerView spawner)
        {
            var data = _data.SkeletonData;
            data.GetSkeletonData(false);
            isInFront = spawner.IsInFront;
            var go = SkeletonAnimation.NewSkeletonAnimationGameObject(data);
            _view = go.gameObject.GetOrAddComponent<CharacterView>();
            var colider = _view.gameObject.GetOrAddComponent<BoxCollider2D>();
            colider.size = spawner.Size;
            colider.offset = new Vector2(0, spawner.Size.y / 2);
            _view.name = _data.InitSkin;
            _view.transform.SetPositionAndRotation(spawner.SpawnPos, Quaternion.identity);
            _team = spawner.Team;
            _view.SetUp(_data, !IsPlayer, this);
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

        public void Attack(Character target)
        {
            target.GetDamage(_data.Dmg);
        }

        public void SwitchPlaces(Character target, Action onComplete)
        {
            isInFront = !isInFront;
            var pos = target.View.transform.position;
            target.View.transform.DOMove(_view.transform.position, 0.3f);
            target.isInFront = !isInFront;
            _view.transform.DOMove(pos, 0.3f).OnComplete(() => onComplete?.Invoke());
        }
        private void Death()
        {
            Debug.Log("Death");
        }
    }
}