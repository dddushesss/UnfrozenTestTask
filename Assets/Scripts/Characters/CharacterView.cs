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
        private SkeletonAnimation animation;
        private CharacterData _data;
        public Action<int> OnHit;

        public void SetUp(CharacterData data)
        {
            animation = gameObject.GetOrAddComponent<SkeletonAnimation>();
            if (animation.state is null) return;
            animation.state.Event += Handle;
        }

        [EditorButton]
        private void SetAnimation(string name, bool loop)
        {
            animation.state.SetAnimation(0, name, loop);
            animation.state.AddAnimation(0, "Idle", true, 0f);
        }

        [EditorButton]
        private void PopUp(float multyplier)
        {
            var defultScale = transform.localScale;

            transform.DOScale(transform.localScale * multyplier, 0.3f).SetEase(Ease.OutQuart);
            Singleton<TimerHelper>.Instance.StartTimer(() => transform.DOScale(defultScale, 0.3f),
                animation.state.GetCurrent(0).AnimationTime + 0.5f);
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
                OnHit.Invoke(_data.Dmg);
            }
        }
    }
}