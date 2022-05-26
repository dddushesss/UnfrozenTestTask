using System;
using Data;
using DG.Tweening;
using Singleton;
using Spine;
using Spine.Unity;
using UnityEngine;
using Event = Spine.Event;


namespace Characters
{
    [RequireComponent(typeof(SkeletonAnimation))]
    public class CharacterView : MonoBehaviour
    {
        private SkeletonAnimation _skeleton;
        private CharacterData _data;
        private MeshRenderer _renderer;
        public Action<int> OnHit;
        
        public void SetUp(CharacterData data, bool isToFlip)
        {
            _skeleton = gameObject.GetOrAddComponent<SkeletonAnimation>();
            _renderer = gameObject.GetComponent<MeshRenderer>();
            if (_skeleton.state is null) return;
            if (isToFlip)
                FlipX();
            _data = data; 
            _skeleton.Initialize(true);
            _skeleton.Skeleton.SetSkin(_data.InitSkin);
            _renderer.sortingOrder = 3;
            _skeleton.AnimationState.SetAnimation(0, _data.InitAnimation, true);
            _skeleton.state.Event += Handle;
            
        }

        public void PlayAnimation(string animation)
        {
            _skeleton.AnimationState.SetAnimation(0, animation, false);
            _skeleton.state.AddAnimation(0, _data.InitAnimation, true, 0f);
        }
        private void FlipX()
        {
            _skeleton.initialFlipX = true;
        }

        [EditorButton]
        private void SetAnimation(string name, bool loop)
        {
            _skeleton.state.SetAnimation(0, name, loop);
            _skeleton.state.AddAnimation(0, "Idle", true, 0f);
        }

        [EditorButton]
        private void PopUp(float multyplier)
        {
            var defultScale = transform.localScale;

            transform.DOScale(transform.localScale * multyplier, 0.3f).SetEase(Ease.OutQuart);
            Singleton<TimerHelper>.Instance.StartTimer(() => transform.DOScale(defultScale, 0.3f),
                _skeleton.state.GetCurrent(0).AnimationTime + 0.5f);
        }

        [EditorButton]
        private void Attack()
        {
            SetAnimation("Miner_1", false);
            PopUp(1.5f);
        }

        private void Handle(TrackEntry trackEntry, Event action)
        {
            if (action.Data.Name == "Hit")
            {
                OnHit?.Invoke(_data.Dmg);
            }
        }
    }
}