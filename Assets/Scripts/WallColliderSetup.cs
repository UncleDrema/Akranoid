using UnityEngine;

namespace Game
{
    public class WallColliderSetup : MonoBehaviour
    {
        [SerializeField]
        private RectTransform wall;

        [SerializeField]
        private BoxCollider2D wallCollider;
        
        private void Update()
        {
            var rect = wall.rect;
            wallCollider.size = rect.size;
        }
    }
}