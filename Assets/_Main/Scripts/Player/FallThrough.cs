using Main.Player;
using UnityEngine;
using Zenject;

namespace Main
{
    public class FallThrough : MonoBehaviour
    {
        [SerializeField] 
        private Collider2D m_playerCollider;
        
        private IInputService m_inputService;
        
        [Inject]
        private void Inject(IInputService inputService) {
            m_inputService = inputService;
        }
        
        private void FixedUpdate()
        {
            m_playerCollider.enabled = !m_inputService.FallInput;
        }
    }
}
