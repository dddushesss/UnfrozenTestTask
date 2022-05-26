using System;
using Data;
using UnityEngine;
using Object = UnityEngine.Object;

namespace StateMachine
{
    public class BattleInterfaceController
    {
        private BattleInterfaceData _data;
        private BattleInterfaceControllerView _view;
        
        public Action OnAttackButtonPressed;
        public Action OnSkipButtonPressed;
        public BattleInterfaceController(BattleInterfaceData data)
        {
            _data = data;
        }

        public BattleInterfaceController Spawn()
        {
            _view = Object.Instantiate(_data.View, Vector3.zero, Quaternion.identity);
            
            _view.AttackButton.onClick.AddListener(() => OnAttackButtonPressed?.Invoke());
            _view.SkipButton.onClick.AddListener(() => OnSkipButtonPressed?.Invoke());
            return this;
        }

        public void SetButtonsEnabled(bool isEnabled)
        {
            _view.AttackButton.interactable = isEnabled;
            _view.SkipButton.interactable = isEnabled;
        }
    }
}