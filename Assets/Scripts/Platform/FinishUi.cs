using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Platform
{
    public class FinishUi : MonoBehaviour
    {
        [SerializeField, Scene]
        private string nextScene;

        [SerializeField]
        private GameObject failPanel;

        [SerializeField]
        private GameObject winPanel;

        public void FailLevel()
        {
            failPanel.SetActive(true);
        }

        public void WinLevel()
        {
            winPanel.SetActive(true);
        }

        public void LoadNextScene()
        {
            SceneManager.LoadScene(nextScene);
        }

        public void TryAgain()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().path);
        }
    }
}