using System;
using UnityEngine;

namespace Game.Platform
{
    public class DestroyWhenOffscreen : MonoBehaviour
    {
        [SerializeField]
        private float deadZoneY = -5.5f;

        private void Update()
        {
            if (transform.position.y < deadZoneY)
            {
                Destroy(gameObject);
            }
        }
    }
}