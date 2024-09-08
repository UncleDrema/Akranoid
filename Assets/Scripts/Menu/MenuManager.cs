using System;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Game.Menu
{
    public class MenuManager : MonoBehaviour
    {
        [Scene, SerializeField]
        private string levelScene;

        [SerializeField]
        private LevelRepository levelRepository;

        [SerializeField]
        private Image previewImage;

        [SerializeField]
        private Button launchButton;

        [SerializeField]
        private Sprite infoSprite;

        [SerializeField]
        private Button infoButton;
        
        public void LaunchGame()
        {
            SceneManager.LoadScene(levelScene);
        }
        
        public void Quit()
        {
            Application.Quit();
        }

        public void SelectLevelOne() => SelectLevel(0);
        
        public void SelectLevelTwo() => SelectLevel(1);
        
        public void SelectLevelThree() => SelectLevel(2);
        
        public void SelectLevelFour() => SelectLevel(3);

        private void Awake()
        {
            levelScene = "";
            ShowInfo();
        }

        public void ShowInfo()
        {
            previewImage.sprite = infoSprite;
            infoButton.interactable = false;
            launchButton.interactable = false;
        }

        public void SelectLevel(int i)
        {
            var entry = levelRepository.Levels[i];
            var preview = entry.Preview;
            previewImage.sprite = preview;
            levelScene = entry.Scene;
            launchButton.interactable = true;
            infoButton.interactable = true;
        }
    }
}