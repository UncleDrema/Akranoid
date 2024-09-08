using System;
using TMPro;
using TriInspector;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Platform
{
    public class LevelUi : MonoBehaviour
    {
        [SerializeField]
        private RacketBehaviour racket;

        [SerializeField]
        private TMP_Text healthText;

        [SerializeField, Scene]
        private string menuScene;

        private void LateUpdate()
        {
            healthText.text = $"LIVES: {racket.Health}";
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}