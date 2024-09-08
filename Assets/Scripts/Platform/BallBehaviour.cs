using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Platform
{
    public class BallBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RectTransform ballRect;

        [SerializeField]
        private CircleCollider2D circleCollider;

        [SerializeField]
        private Rigidbody2D rb;
        
        [Range(0.2f, 0.9f)]
        [SerializeField]
        float bounceFudge = 0.4f;
        
        public bool IsLaunched { get; private set; }

        private void Awake()
        {
            rb.isKinematic = true;
        }

        public void Launch(RacketBehaviour fromRacket = null)
        {
            if (IsLaunched)
                return;

            rb.isKinematic = false;
            IsLaunched = true;

            float angle;
            if (fromRacket != null)
            {
                var directionFromRacket = transform.position - fromRacket.transform.position;
                directionFromRacket.Normalize();
                angle = Mathf.Atan2(directionFromRacket.y, directionFromRacket.x);
            }
            else
            {
                const float delta = Mathf.PI / 8;
                const float angleWidth = Mathf.PI - 2 * delta;
                angle = delta + Random.value * angleWidth;
            }
            LaunchWithAngle(angle);
        }

        public void StopBall()
        {
            if (!IsLaunched)
                return;

            rb.isKinematic = true;
            IsLaunched = false;
            rb.velocity = Vector2.zero;
        }

        private void Update()
        {
            circleCollider.radius = ballRect.rect.width / 2;
        }
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Prevent ball from following paths that are too close to horizontal
            // or vertical (to prevent boring loops)

            // Get the ball's velocity (after a bounce)

            float ballAngle;
            if (collision.gameObject.TryGetComponent(out RacketBehaviour racket))
            {
                if (racket.CoveredInGlue)
                {
                    racket.GlueBall(this);
                    return;
                }
                else
                {
                    var directionFromRacket = transform.position - racket.transform.position;
                    directionFromRacket.Normalize();
                    ballAngle = Mathf.Atan2(directionFromRacket.y, directionFromRacket.x);
                }
            }
            else
            {

                // Calculate the angle (in radians)
                Vector2 ballVector = rb.velocity;
                ballAngle = Mathf.Atan2(ballVector.y, ballVector.x);

                if (ballAngle >= 0.0f && ballAngle < bounceFudge)
                {
                    ballAngle = bounceFudge;
                }
                else if (ballAngle <= Mathf.PI * 0.5f && ballAngle > Mathf.PI * 0.5f - bounceFudge)
                {
                    ballAngle = Mathf.PI * 0.5f - bounceFudge;
                }
                else if (ballAngle > Mathf.PI * 0.5f && ballAngle < Mathf.PI * 0.5f + bounceFudge)
                {
                    ballAngle = Mathf.PI * 0.5f + bounceFudge;
                }
                else if (ballAngle <= Mathf.PI && ballAngle > Mathf.PI - bounceFudge)
                {
                    ballAngle = Mathf.PI - bounceFudge;
                }
                else if (ballAngle < bounceFudge - Mathf.PI)
                {
                    ballAngle = bounceFudge - Mathf.PI;
                }
                else if (ballAngle <= -Mathf.PI * 0.5f && ballAngle > -bounceFudge - Mathf.PI * 0.5f)
                {
                    ballAngle = -bounceFudge - Mathf.PI * 0.5f;
                }
                else if (ballAngle > -Mathf.PI * 0.5f && ballAngle < bounceFudge - Mathf.PI * 0.5f)
                {
                    ballAngle = bounceFudge - Mathf.PI * 0.5f;
                }
                else if (ballAngle < 0.0f && ballAngle > -bounceFudge)
                {
                    ballAngle = -bounceFudge;
                }
            }

            // NOTE: Also constrain the ball's velocity to its initial value
            // to avoid unexpected changes of speed
        
            LaunchWithAngle(ballAngle);
        }

        private void LaunchWithAngle(float ballAngle)
        {
            Vector2 ballVector;
            ballVector.x = Mathf.Cos(ballAngle) * GameManager.Racket.BallSpeed;
            ballVector.y = Mathf.Sin(ballAngle) * GameManager.Racket.BallSpeed;
            rb.velocity = ballVector;
        }
    }
}