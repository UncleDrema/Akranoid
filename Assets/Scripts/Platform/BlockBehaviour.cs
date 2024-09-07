using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Platform
{
    public class BlockBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> healthStates;

        private int maxHealth;
        private int health;

        private void Awake()
        {
            maxHealth = healthStates.Count;
            health = maxHealth;
        }

        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.TryGetComponent(out BallBehaviour ball))
            {
                Debug.Log($"Bounce!");
                Damage();
            }
        }

        private void SetCurrentHealthStateActive(bool isActive)
        {
            healthStates[maxHealth - health].SetActive(isActive);
        }

        private void Damage()
        {
            SetCurrentHealthStateActive(false);
            health--;
            if (health <= 0)
            {
                Destroy(gameObject);
            }
            else
            {
                SetCurrentHealthStateActive(true);
            }
        }
    }
}