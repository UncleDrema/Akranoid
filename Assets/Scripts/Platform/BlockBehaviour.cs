using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Platform
{
    public class BlockBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> healthStates;

        [SerializeField]
        private List<GameObject> bonusPrefabs;

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
                Damage();
            }
        }

        private void SetCurrentHealthStateActive(bool isActive)
        {
            var i = maxHealth - health;
            if (i >= 0 & i < healthStates.Count)
            {
                healthStates[i].SetActive(isActive);
            }
        }

        private void Damage()
        {
            SetCurrentHealthStateActive(false);
            health--;
            if (health <= 0)
            {
                Die();
            }
            else
            {
                SetCurrentHealthStateActive(true);
            }
        }

        private void Die()
        {
            if (bonusPrefabs.Count > 0)
            {
                var prefab = bonusPrefabs[Random.Range(0, bonusPrefabs.Count)];
                var bonus = CanvasManager.InstantiateObject(prefab, transform.position);
            }
            Destroy(gameObject);
        }
    }
}