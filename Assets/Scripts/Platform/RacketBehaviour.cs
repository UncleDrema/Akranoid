using System;
using Game.Platform.Bonuses;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Platform
{
    public class RacketBehaviour : MonoBehaviour
    {
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
                var ball = CanvasManager.InstantiateObject(ballPrefab, ballHolder.position);
                ball.Launch();
            }
        }

        public void DuplicateBalls()
        {
            foreach (var ball in FindObjectsOfType<BallBehaviour>())
            {
                var newBall = CanvasManager.InstantiateObject(ballPrefab, ball.transform.position);
                newBall.Launch();
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.TryGetComponent(out BonusBase bonus))
            {
                bonus.Use(this);
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