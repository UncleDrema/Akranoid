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

        [SerializeField]
        private TMP_Text blockText;

        private void LateUpdate()
        {
            healthText.text = $"LIVES: {racket.Health}";
            blockText.text = $"BLOCKS LEFT: {FindObjectsOfType<BlockBehaviour>().Length}";
        }

        public void ReturnToMenu()
        {
            SceneManager.LoadScene(menuScene);
        }
    }
}