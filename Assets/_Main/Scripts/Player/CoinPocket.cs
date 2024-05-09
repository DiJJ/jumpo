using System;
using UnityEngine;

namespace Main
{
    public class CoinPocket : MonoBehaviour
    {
        [SerializeField] 
        private ParticleSystem m_particleSystem;
        [SerializeField] 
        private int m_coinDropRate = 10;
        
        public int Coins { get; private set; }
        public event Action<int> coinCountUpdated = (count) => { };

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.TryGetComponent(out Coin coin))
            {
                Coins++;
                coinCountUpdated.Invoke(Coins);
            }
        }

        public void DropCoins()
        {
            Coins -= m_coinDropRate;
            if (Coins < 0)
                Coins = 0;
            Instantiate(m_particleSystem, transform.position, Quaternion.identity).Play();
            coinCountUpdated.Invoke(Coins);
        }
    }
}
