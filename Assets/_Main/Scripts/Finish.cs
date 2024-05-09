using UnityEngine;
using Zenject;

namespace Main
{
    public class Finish : MonoBehaviour
    {
        private IGameUI m_gameUI;
        
        [Inject]
        private void Inject(IGameUI gameUI)
        {
            m_gameUI = gameUI;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Player.Player player))
            {
                player.enabled = false;
                m_gameUI.ShowComplete();
                Destroy(gameObject);
            }
        }
    }
}
