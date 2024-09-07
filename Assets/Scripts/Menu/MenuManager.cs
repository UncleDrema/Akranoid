using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Scene, SerializeField]
        private string levelScene;
        
        public void LaunchGame()
        {
            SceneManager.LoadScene(levelScene);
        }
        
        public void Quit()
        {
            Application.Quit();
        }
    }
}