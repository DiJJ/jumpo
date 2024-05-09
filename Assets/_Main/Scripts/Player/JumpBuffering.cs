using System.Collections;
using UnityEngine;
using Zenject;

namespace Main.Player {
    public class JumpBuffering : MonoBehaviour {
        [SerializeField]
        private float m_bufferingTime = 0.15f;
        
        private Coroutine m_jumpBufferCoroutine;
        private IInputService m_inputService;
        private Player m_player;

        private WaitForSeconds m_waitForSeconds; 
        
        public bool IsJumpBuffered { get; private set; }
        
        [Inject]
        private void Inject(IInputService inputService) {
            m_inputService = inputService;
        }

        private void Awake() {
            m_player = GetComponent<Player>();
            m_waitForSeconds = new WaitForSeconds(m_bufferingTime);
        }

        private void BufferJump() {
            if (m_jumpBufferCoroutine != null)
                StopCoroutine(m_jumpBufferCoroutine);
            m_jumpBufferCoroutine = StartCoroutine(JumpBufferCoroutine());
        }

        private void ClearBuffer() {
            if (m_jumpBufferCoroutine != null)
                StopCoroutine(m_jumpBufferCoroutine);
            IsJumpBuffered = false;
        }
        
        private IEnumerator JumpBufferCoroutine()
        {
            IsJumpBuffered = true;
            yield return m_waitForSeconds;
            IsJumpBuffered = false;
        }
        
        
        private void OnEnable()
        {
            m_inputService.OnJump += BufferJump;
            m_player.OnJumpPerformed += ClearBuffer;
            m_player.OnWallJumpPerformed += ClearBuffer;
        }

        private void OnDisable()
        {
            m_inputService.OnJump -= BufferJump;
            m_player.OnJumpPerformed -= ClearBuffer;
            m_player.OnWallJumpPerformed -= ClearBuffer;
        }
    }
}
