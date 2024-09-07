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
        private Vector2 initialForce;

        [SerializeField]
        private Rigidbody2D rb;
        
        private bool _isLaunched;

        private void Awake()
        {
            rb.isKinematic = true;
        }

        public void Launch()
        {
            if (_isLaunched)
                return;

            rb.isKinematic = false;
            _isLaunched = true;
            LaunchWithAngle(Random.value * Mathf.PI);
        }

        private void Update()
        {
            circleCollider.radius = ballRect.rect.width / 2;
        }
        
        [Range(7.0f,15.0f)] [SerializeField] float pushSpeed = 12.0f;
        [Range(0.2f, 0.9f)] [SerializeField] float bounceFudge = 0.4f;
        
        private void OnCollisionEnter2D(Collision2D collision)
        {
            // Prevent ball from following paths that are too close to horizontal
            // or vertical (to prevent boring loops)

            // Get the ball's velocity (after a bounce)

            float ballAngle;
            if (collision.gameObject.TryGetComponent(out RacketBehaviour racket))
            {
                Debug.Log($"Racket!");
                var directionFromRacket = transform.position - racket.transform.position;
                directionFromRacket.Normalize();
                ballAngle = Mathf.Atan2(directionFromRacket.y, directionFromRacket.x);
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
            ballVector.x = Mathf.Cos(ballAngle) * pushSpeed;
            ballVector.y = Mathf.Sin(ballAngle) * pushSpeed;
            rb.velocity = ballVector;
        }
    }
}