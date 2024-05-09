using System;
using System.Collections;
using Main.Player;
using UnityEngine;
using Zenject;

namespace Main
{
    public class Platform : MonoBehaviour
    {
        [SerializeField]
        private Collider2D m_collider;
        private IInputService m_inputService;

        [Inject]
        private void Inject(IInputService inputService)
        {
            m_inputService = inputService;
        }
        
        private void Start()
        {
            m_inputService.OnFall += Disable;
        }

        private void Disable()
        {
            StartCoroutine(DisableCoroutine());
        }

        private IEnumerator DisableCoroutine()
        {
            m_collider.enabled = false;
            yield return new WaitForSeconds(0.2f);
            m_collider.enabled = true;
        }
        
        private void OnEnable()
        {
            if (m_inputService != null)
                m_inputService.OnFall += Disable;
        }
        
        private void OnDisable()
        {
            if (m_inputService != null)
                m_inputService.OnFall -= Disable;
        }
    }
}
