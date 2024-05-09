using System.Collections;
using UnityEngine;

namespace Main.Player {
    public class CoyoteTime : MonoBehaviour {
        [SerializeField]
        private float m_coyoteTime = 0.15f;

        private Coroutine m_coyoteTimeCoroutine;
        private Player m_player;
        private bool m_isAvailable;

        private WaitForSeconds m_waitForSeconds;

        public bool IsItCoyoteTime { get; private set; }

        private void Awake() {
            m_player = GetComponent<Player>();
            m_player.OnJumpPerformed += () =>
            {
                m_isAvailable = false;
                StopCoyoteTime();
            };
            m_player.OnGroundedChanged += UpdateIsGrounded;
            m_waitForSeconds = new WaitForSeconds(m_coyoteTime);
        }

        private IEnumerator CoyoteTimeCoroutine() {
            IsItCoyoteTime = true;
            yield return m_waitForSeconds;
            IsItCoyoteTime = false;
        }
        
        private void UpdateIsGrounded(bool isGrounded) {
            if (isGrounded)
            {
                StopCoyoteTime();
                m_isAvailable = true;
                return;
            }
            
            if (m_isAvailable)
            {
                StartCoyoteTime();
            }
        }
        
        private void StartCoyoteTime() {
            if (m_coyoteTimeCoroutine != null)
                return;
            
            m_coyoteTimeCoroutine = StartCoroutine(CoyoteTimeCoroutine());
        }
        
        private void StopCoyoteTime() {
            if (m_coyoteTimeCoroutine == null)
                return;
            
            StopCoroutine(m_coyoteTimeCoroutine);
            IsItCoyoteTime = false;
            m_coyoteTimeCoroutine = null;
        }
    }
}
