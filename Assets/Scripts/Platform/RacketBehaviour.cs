using System.Collections.Generic;
using System.Linq;
using Game.Platform.Bonuses;
using TriInspector;
using UnityEngine;

namespace Game.Platform
{
    public class RacketBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RectTransform racketTransform;
        
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

        [SerializeField]
        private List<int> sizes = new List<int>() { 150, 300, 450};

        [SerializeField]
        private int sizeIndex = 1;
        
        [SerializeField]
        private List<int> speeds = new List<int>() { 6, 9, 12};

        [SerializeField]
        private int speedIndex = 1;

        private List<(BallBehaviour, Vector3)> gluedBalls = new();

        public bool CoveredInGlue => glueTimer > 0;

        public float GlueTimer => glueTimer;

        private float glueTimer = 0;

        [field: SerializeField]
        public int Health { get; private set; } = 3;
        
        [field: SerializeField]
        public int BallSpeed { get; private set; } = 3;

        private bool gameActive = false;

        public bool CanScaleUp => sizeIndex < sizes.Count - 1;
        public bool CanScaleDown => sizeIndex > 0;
        
        public bool CanSpeedUp => speedIndex < speeds.Count - 1;
        public bool CanSpeedDown => speedIndex > 0;

        [Button]
        public void ScaleUp()
        {
            if (!CanScaleUp)
                return;

            sizeIndex++;
            var size = racketTransform.sizeDelta;
            size.x = sizes[sizeIndex];
            racketTransform.sizeDelta = size;
        }
        
        [Button]
        public void ScaleDown()
        {
            if (!CanScaleDown)
                return;

            sizeIndex--;
            var size = racketTransform.sizeDelta;
            size.x = sizes[sizeIndex];
            racketTransform.sizeDelta = size;
        }
        
        [Button]
        public void SpeedUp()
        {
            if (!CanSpeedUp)
                return;

            speedIndex++;
            BallSpeed = speeds[speedIndex];
        }
        
        [Button]
        public void SpeedDown()
        {
            if (!CanSpeedDown)
                return;

            speedIndex--;
            BallSpeed = speeds[speedIndex];
        }

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
            UpdateBlocks();
        }

        private void UpdateBlocks()
        {
            if (FindAnyObjectByType<BlockBehaviour>() is null)
            {
                Win();
            }
        }

        private void ClearLevel()
        {
            foreach (var ball in FindObjectsOfType<BallBehaviour>().ToList())
            {
                ball.StopBall();
            }

            foreach (var bonus in FindObjectsOfType<BonusBehaviour>().ToList())
            {
                Destroy(bonus);
            }
        }

        private void Win()
        {
            ClearLevel();
            GameManager.WinLevel();
        }

        private void UpdateHealth()
        {
            if (!gameActive)
                return;
            
            if (FindAnyObjectByType<BallBehaviour>() is null)
            {
                Damage();
            }
        }

        public void AddHealth()
        {
            Health++;
        }

        private void Damage()
        {
            gameActive = false;
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
            ClearLevel();
            GameManager.FailLevel();
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
            var ball = GameManager.InstantiateObject(ballPrefab, ballHolder.position);
            GlueBall(ball);
            gameActive = true;
        }

        private void Start()
        {
            BallSpeed = speeds[speedIndex];
            SpawnBallOnRacket();
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
                var newBall = GameManager.InstantiateObject(ballPrefab, ball.transform.position);
                
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
            if (other.gameObject.TryGetComponent(out BonusBehaviour bonus))
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