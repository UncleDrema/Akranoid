using System;
using UnityEngine;

namespace Game
{
    public class UiColliderSetup : MonoBehaviour
    {
        [SerializeField]
        private RectTransform uiRect;

        [SerializeField]
        private BoxCollider2D uiCollider;
        
        private void Update()
        {
           UpdateCollider();
        }

        private void OnValidate()
        {
            UpdateCollider();
        }

        private void UpdateCollider()
        {
            var rect = uiRect.rect;
            uiCollider.size = rect.size;
        }
    }
}