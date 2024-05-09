using Main.Player;
using UnityEngine;
using Zenject;

namespace Main
{
    [RequireComponent(typeof(Player.Player))]
    public class AnimationController : MonoBehaviour {
        [SerializeField]
        private Animator m_animator;
        [SerializeField]
        private Animator m_dustJumpAnimator;
        [SerializeField]
        private Animator m_dustFallAnimator;
        
        private Player.Player m_player;
        private IInputService m_inputService;
        
        [Inject]
        private void Inject(IInputService inputService) {
            m_inputService = inputService;
        }

        private void Awake() {
            m_player = GetComponent<Player.Player>();
            m_player.OnGroundedChanged += UpdateGrounded;
            m_player.OnJumpPerformed += Jump;
            m_player.OnWallJumpPerformed += Jump;
            m_player.OnDie += () => m_animator.SetTrigger("die");
            m_player.OnRespawn += () => m_animator.SetTrigger("alive");
        }

        private void Jump()
        {
            m_animator.SetTrigger("jump");
            Instantiate(m_dustJumpAnimator, transform.position + Vector3.down * .1f, Quaternion.identity);
        }

        private void UpdateGrounded(bool grounded)
        {
            if (m_animator.GetBool("isGrounded") == false && grounded == true)
            {
                Instantiate(m_dustFallAnimator, transform.position + Vector3.down * .11f, Quaternion.identity);
            }
            m_animator.SetBool("isGrounded", grounded);
        }

        private void Update() {
            float dir = m_inputService.MoveInput;
            if (dir != 0)
                transform.right = Vector2.right * dir;
            m_animator.SetFloat("speed", Mathf.Abs(dir));
            m_animator.SetFloat("verticalSpeed", m_player.Velocity.y);
        }
    }
}
