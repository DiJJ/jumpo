using UnityEngine;

namespace Main
{
    public class Chain : MonoBehaviour
    {
        [SerializeField] private Animator m_animator;
        [SerializeField] private AnimationClip m_animationClip;

        private void Start()
        {
            m_animator.Play(m_animationClip.name, 0, (transform.position.x - .5f) % 10f * 0.1f);
        }
    }
}
