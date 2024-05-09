using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(Player.Player))]
    public class AudioController : MonoBehaviour {
        [SerializeField] private AudioSource m_oneShotSource;
        [SerializeField] private AudioSource m_loopSource;

        [SerializeField] private AudioClip m_jumpClip;
        [SerializeField] private AudioClip m_dieClip;
        [SerializeField] private AudioClip m_runLoop;
        [SerializeField] private AudioClip m_slideLoop;

        private Player.Player m_player;
        
        private void Awake() {
            m_player = GetComponent<Player.Player>();
            m_player.OnJumpPerformed += () => m_oneShotSource.PlayOneShot(m_jumpClip);
            m_player.OnWallJumpPerformed += () => m_oneShotSource.PlayOneShot(m_jumpClip);
            m_player.OnDie += () => m_oneShotSource.PlayOneShot(m_dieClip);
        }

        private void Update()
        {
            if (m_player.IsGrounded)
            {
                if (m_player.Velocity.magnitude >= 1f)
                {
                    m_loopSource.clip = m_runLoop;
                    if (!m_loopSource.isPlaying)
                        m_loopSource.Play();
                }
                else
                {
                    m_loopSource.Stop();
                }
            }
            else
            {
                if (m_player.IsWalled)
                {
                    m_loopSource.clip = m_slideLoop;
                    if (!m_loopSource.isPlaying)
                        m_loopSource.Play();
                }
                else
                {
                    m_loopSource.Stop();
                }
            }
        }
    }
}
