using UnityEngine;
using UnityEngine.SceneManagement;

namespace Main
{
    public class StartMenu : MonoBehaviour
    {
        
        public void PlayGame()
        {
            SceneManager.LoadScene(1);
        }

        public void CloseGame()
        {
            Application.Quit();
        }
    }
}
