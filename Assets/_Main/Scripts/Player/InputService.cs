using System;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Main.Player
{
    public class InputService : MonoBehaviour, IInputService {
        [FoldoutGroup("Actions")]
        [SerializeField] private InputActionReference m_moveAction;
        [FoldoutGroup("Actions")]
        [SerializeField] private InputActionReference m_jumpAction;
        [FoldoutGroup("Actions")]
        [SerializeField] private InputActionReference m_fallAction;

        public float MoveInput => m_moveAction.action.ReadValue<float>();
        public bool FallInput => m_fallAction.action.IsPressed();
        public event Action<float> OnMove = (direction) => { };
        public event Action OnJump = () => { };
        public event Action OnFall = () => { };

        private void Awake() {
            m_moveAction.action.performed += context => OnMove.Invoke(MoveInput);
            m_jumpAction.action.performed += context => OnJump.Invoke();
            m_fallAction.action.performed += context => OnFall.Invoke();
        }

        private void OnEnable() {
            m_moveAction.action.Enable();
            m_jumpAction.action.Enable();
            m_fallAction.action.Enable();
        }
        
        private void OnDisable() {
            m_moveAction.action.Disable();
            m_jumpAction.action.Disable();
            m_fallAction.action.Disable();
        }
    }

    public interface IInputService {
        public float MoveInput { get; }
        public bool FallInput { get; }
        public event Action<float> OnMove;
        public event Action OnJump;
        public event Action OnFall;
    }
}
