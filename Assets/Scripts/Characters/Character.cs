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
        private Vector3 _defaultPosition;

        public bool IsPlayer => _team is Team.Player;
        public CharacterView View => _view;

        public Vector3 DefaultPosition => _defaultPosition;

        public Character(CharacterData data)
        {
            _data = data;
        }

        public void Spawn(SpawnerView spawner)
        {
            var data = _data.SkeletonData;
            data.GetSkeletonData(false);
            _defaultPosition = spawner.SpawnPos;
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
            _view.PlayAnimation(_data.AttackAnimation);
            _view.OnHit += target.GetDamage;
            
        }
        
        private void Death()
        {
            Debug.Log("Death");
        }
    }
}