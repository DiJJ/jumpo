using System.Collections;
using UnityEngine;

namespace Main.Player {
    public class CoyoteTime : MonoBehaviour {
        [SerializeField]
        private float m_coyoteTime;

        private bool m_jumpAvailable;

        public bool JumpAvailable => m_jumpAvailable;
        
        private IEnumerator CoyoteTimeCoroutine() {
            m_jumpAvailable = true;
            yield return new WaitForSeconds(m_coyoteTime);
            m_jumpAvailable = false;
        }

        public void UpdateIsGrounded(bool isGrounded) {
            
        }
    }
}
