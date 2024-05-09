using System.Collections;
using UnityEngine;

namespace Main
{
    public class DustAnimator : MonoBehaviour
    {
        [SerializeField] 
        private float m_lifeTime;
        
        void Start()
        {
            StartCoroutine(LifeTime());
        }

        private IEnumerator LifeTime()
        {
            yield return new WaitForSeconds(m_lifeTime);
            Destroy(gameObject);
        }
    }
}
