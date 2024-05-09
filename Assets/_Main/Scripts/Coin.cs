using UnityEngine;

namespace Main
{
    [RequireComponent(typeof(CircleCollider2D))]
    public class Coin : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer m_spriteRenderer;
        [SerializeField] private Animator m_animator;
        [SerializeField] private AudioSource m_coinAudioSource;
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                m_coinAudioSource.Play();
                m_animator.SetTrigger("pickedUp");
            }
        }

        public void DisableCoin()
        {
            m_spriteRenderer.enabled = false;
            GetComponent<Collider2D>().enabled = false;
        }
    }
}
