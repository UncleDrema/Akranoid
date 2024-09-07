using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Platform
{
    public class CanvasManager : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform root;
        
        public static CanvasManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static T InstantiateObject<T>(T prefab, Vector3 position)
        where T : Component
        {
            return Instance.InstantiateObjectInner(prefab, position);
        }

        private T InstantiateObjectInner<T>(T prefab, Vector3 position)
        where T : Component
        {
            var go = Instantiate(prefab, root, true);
            go.transform.position = position;
            go.transform.localScale = Vector3.one;
            return go;
        }
        
        public static GameObject InstantiateObject(GameObject prefab, Vector3 position)
        {
            return Instance.InstantiateObjectInner(prefab, position);
        }

        private GameObject InstantiateObjectInner(GameObject prefab, Vector3 position)
        {
            var go = Instantiate(prefab, root, true);
            go.transform.position = position;
            go.transform.localScale = Vector3.one;
            return go;
        }
    }
}