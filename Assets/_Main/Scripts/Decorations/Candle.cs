using UnityEngine;

namespace Main
{
    public class Candle : MonoBehaviour
    {
        [SerializeField] private Animator m_candleAnimator;
        [SerializeField] private Animator m_lightAnimator;

        private void Start()
        {
            m_candleAnimator.Play("candle", 0, (transform.position.x - .5f) % 2f * 0.5f);
            m_lightAnimator.Play("candleLight", 0, (transform.position.x - .5f) % 2f * 0.5f);
        }
    }
}
