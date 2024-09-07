using System.Collections.Generic;
using System.Linq;
using Game.Platform.Bonuses;
using UnityEngine;

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

        [SerializeField]
        private GameObject glueOverlay;

        private List<(BallBehaviour, Vector3)> gluedBalls = new();

        public bool CoveredInGlue => glueTimer > 0;

        private float glueTimer = 0;

        [field: SerializeField]
        public int MaxHealth { get; private set; } = 3;
        
        [field: SerializeField]
        public int Health { get; private set; }

        public void CoverWithGlue(float time)
        {
            if (!CoveredInGlue)
            {
                glueTimer = time;
            }
            else
            {
                glueTimer += time;
            }
        }

        public void GlueBall(BallBehaviour ball)
        {
            var offset = ball.transform.position - ballHolder.position;
            gluedBalls.Add((ball, offset));
            ball.StopBall();
        }

        private void Update()
        {
            MoveRacket();
            Input();
            UpdateGlue();
            UpdateGlueGraphics();
            UpdateHealth();
        }

        private void UpdateHealth()
        {
            if (FindAnyObjectByType<BallBehaviour>() is null)
            {
                Damage();
            }
        }

        private void AddHealth()
        {
            if (Health < MaxHealth)
                Health++;
        }

        private void Damage()
        {
            Health--;
            if (Health <= 0)
            {
                Die();
            }
            else
            {
                SpawnBallOnRacket();
            }
        }

        private void Die()
        {
            
        }

        private void UpdateGlue()
        {
            if (glueTimer > 0 && (glueTimer -= Time.deltaTime) < 0)
            {
                LaunchAllBalls();
            }
        }

        private void UpdateGlueGraphics()
        {
            if (glueOverlay.activeSelf != CoveredInGlue)
            {
                glueOverlay.SetActive(CoveredInGlue);
            }
        }

        private void SpawnBallOnRacket()
        {
            var ball = CanvasManager.InstantiateObject(ballPrefab, ballHolder.position);
            GlueBall(ball);
        }

        private void Start()
        {
            SpawnBallOnRacket();
            Health = MaxHealth;
        }

        private void UpdateGluedBalls()
        {
            for (int i = gluedBalls.Count - 1; i >= 0; i--)
            {
                if (gluedBalls[i].Item1 == null)
                {
                    gluedBalls.RemoveAt(i);
                }
            }
            foreach (var (ball, offset) in gluedBalls)
            {
                ball.transform.position = ballHolder.transform.position + offset;
            }
        }

        private void LateUpdate()
        {
            if (gluedBalls.Count > 0)
            {
                UpdateGluedBalls();
            }
        }

        private void LaunchAllBalls()
        {
            foreach (var (ball, _) in gluedBalls)
            {
                if (CoveredInGlue)
                    ball.Launch(this);
                else
                    ball.Launch();
            }
            gluedBalls.Clear();
        }

        private void Input()
        {
            if (UnityEngine.Input.GetKeyDown(KeyCode.Space) || UnityEngine.Input.GetKeyDown(KeyCode.Mouse0))
            {
                LaunchAllBalls();
            }
        }

        public void DuplicateBalls()
        {
            foreach (var ball in FindObjectsOfType<BallBehaviour>().ToList())
            {
                var newBall = CanvasManager.InstantiateObject(ballPrefab, ball.transform.position);
                
                Debug.Log($"Duplicating ball, which is glued: {ball.IsLaunched}");
                if (!ball.IsLaunched)
                {
                    GlueBall(newBall);
                }
                else
                {
                    newBall.Launch();
                }
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
            var pos = UnityEngine.Input.mousePosition;
            if (!Screen.safeArea.Contains(pos))
            {
                return;
            }
            pos.z = playerCamera.nearClipPlane;
            Vector2 mousePos= playerCamera.ScreenToWorldPoint(pos);
            var uiPos = racketArea.transform.InverseTransformPoint(mousePos);

            var corner = racketArea.rect.position;
            var end = corner + racketArea.rect.size;
            Vector3 clampedUiPos = new Vector3(
                Mathf.Clamp(uiPos.x, corner.x, end.x),
                Mathf.Clamp(uiPos.y, corner.y, end.y),
                uiPos.z);

            var clampedMousePos = racketArea.transform.TransformPoint(clampedUiPos);
            
            var curPos = transform.position;
            curPos.x = clampedMousePos.x;
            transform.position = curPos;
        }
    }
}