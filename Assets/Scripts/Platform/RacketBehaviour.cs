using System;
using UnityEngine;

namespace Game.Platform
{
    public class RacketBehaviour : MonoBehaviour
    {
        [SerializeField]
        private float movementArea;

        [SerializeField]
        private Camera playerCamera;

        [SerializeField]
        private Transform ballHolder;

        [SerializeField]
        private BallBehaviour ballPrefab;

        [SerializeField]
        private RectTransform racketArea;

        private void Update()
        {
            MoveRacket();
            LaunchBalls();
        }

        private void LaunchBalls()
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                var ball = Instantiate(ballPrefab, ballHolder);
                ball.Launch();
            }
        }
        
        private void MoveRacket()
        {
            var pos = Input.mousePosition;
            if (!Screen.safeArea.Contains(pos))
            {
                return;
            }
            pos.z = playerCamera.nearClipPlane;
            Vector2 mousePos= playerCamera.ScreenToWorldPoint(pos);
            var uiPos = racketArea.transform.InverseTransformPoint(mousePos);
            
            if (!racketArea.rect.Contains(uiPos))
                return;
            
            var curPos = transform.position;
            curPos.x = mousePos.x;
            transform.position = curPos;
        }
    }
}