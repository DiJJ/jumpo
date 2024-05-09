using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class GameUI : MonoBehaviour, IGameUI
    {
        [SerializeField] private GameObject m_gameCompleteUI;
        [SerializeField] private TextMeshProUGUI m_statusText;
        [SerializeField] private AudioSource m_source;
        [SerializeField] private TextMeshProUGUI m_coinCountText;

        [SerializeField] private CoinPocket m_coinPocket;

        private void Start()
        {
            Hide();
            m_coinPocket.coinCountUpdated += UpdateCoinCount;
        }
        
        private void UpdateCoinCount(int count)
        {
            m_coinCountText.text = $"{count}";
        }

        public void ShowComplete()
        {
            m_statusText.text = "Level Complete!";
            m_gameCompleteUI.SetActive(true);
            m_source.Play();
        }
        
        public void ShowDead()
        {
            m_statusText.text = "You are dead!";
            m_gameCompleteUI.SetActive(true);
            m_source.Play();
        }

        public void Hide()
        {
            m_gameCompleteUI.SetActive(false);
            m_source.Play();
        }
        
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }

    public interface IGameUI
    {
        public void ShowComplete();
        public void ShowDead();
        public void Hide();
    }
}
