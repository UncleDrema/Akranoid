using System.Collections.Generic;
using Game.Platform.Bonuses;
using UnityEngine;

namespace Game.Platform
{
    public class BlockBehaviour : MonoBehaviour
    {
        [SerializeField]
        private List<GameObject> healthStates;

        [SerializeField, Range(0f, 1f)]
        private float bonusChance;
        

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
            var blockCount = FindObjectsOfType<BlockBehaviour>().Length;
            if ((blockCount > 33 && Random.value <= bonusChance * 3) ||
                (Random.value <= bonusChance && blockCount > 2))
            {
                GameManager.SpawnBonus(transform.position);
            }
            Destroy(gameObject);
        }
    }
}