using System;
using Data;
using DG.Tweening;
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
        private Character _character;
        private MeshRenderer _renderer;
        private bool _canChoose = false;

        public MeshRenderer Renderer => _renderer;

        public event Action<int> OnHit;
        public event Action OnAnimationFinished;
        public event Action<Character> OnCharacterChosen;


        public void SetUp(CharacterData data, bool isToFlip, Character character)
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
            _character = character;
            
        }

        public void CanChoose(bool canChoose)
        {
            _canChoose = canChoose;
        }

        public void PlayAnimation(string animation)
        {
            var anim = _skeleton.AnimationState.SetAnimation(0, animation, false);
            anim.Complete += (track) =>
            {
                OnAnimationFinished?.Invoke();
                OnAnimationFinished = null;
            };
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
        private void PopUp(float scale)
        {
            transform.DOScale(scale, 0.3f).SetEase(Ease.OutQuart);
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
                OnHit = null;
            }
        }


        private void OnMouseEnter()
        {
            if (_canChoose)
                PopUp(1.2f);
        }

        private void OnMouseExit()
        {
            if (_canChoose)
                PopUp(1f);
        }

        private void OnMouseUp()
        {
            OnCharacterChosen?.Invoke(_character);
            PopUp(1f);
        }
    }
}