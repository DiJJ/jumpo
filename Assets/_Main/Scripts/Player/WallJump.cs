using System.Collections;
using UnityEngine;

namespace Main
{
    public class WallJump : MonoBehaviour
    {
        [SerializeField]
        private float m_jumpTime = 0.15f;
        
        private Coroutine m_lockMovementCoroutine;
        private Player.Player m_player;

        private WaitForSeconds m_waitForSeconds; 
        
        public bool MovementLocked { get; private set; }

        private void Awake() {
            m_player = GetComponent<Player.Player>();
            m_player.OnWallJumpPerformed += LockMovement;
            m_waitForSeconds = new WaitForSeconds(m_jumpTime);
        }

        private void LockMovement() {
            if (m_lockMovementCoroutine != null)
                StopCoroutine(m_lockMovementCoroutine);
            m_lockMovementCoroutine = StartCoroutine(LockMovementCoroutine());
        }
        
        private IEnumerator LockMovementCoroutine()
        {
            MovementLocked = true;
            yield return m_waitForSeconds;
            MovementLocked = false;
        }
    }
}
