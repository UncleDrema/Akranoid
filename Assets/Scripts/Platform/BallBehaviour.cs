using System;
using UnityEngine;

namespace Game.Platform
{
    public class BallBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Vector2 initialForce;

        [SerializeField]
        private Rigidbody2D rb;

        [SerializeField]
        private float deadZoneY = -5.5f;
        
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
            transform.SetParent(null);
            rb.AddForce(initialForce);
        }

        private void Update()
        {
            if (transform.position.y < deadZoneY)
            {
                Destroy(gameObject);
            }

            if (rb.velocity.sqrMagnitude < 1)
            {
                rb.velocity *= 10;
            }
        }
    }
}