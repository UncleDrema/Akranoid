using System;
using TMPro;
using UnityEngine;

namespace Game.Platform
{
    public class LevelUi : MonoBehaviour
    {
        [SerializeField]
        private RacketBehaviour racket;

        [SerializeField]
        private TMP_Text healthText;

        private void LateUpdate()
        {
            healthText.text = $"{racket.Health} / {racket.MaxHealth}";
        }
    }
}