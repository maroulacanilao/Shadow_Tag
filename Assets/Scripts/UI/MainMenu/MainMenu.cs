using UnityEngine;

namespace UI.MainMenu
{
    public class MainMenu : MonoBehaviour
    {
        [NaughtyAttributes.Scene] [SerializeField] private string gameScene;

        public void Play()
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene(gameScene);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}
