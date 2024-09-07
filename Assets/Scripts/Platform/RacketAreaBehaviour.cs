using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Platform
{
    public class RacketAreaBehaviour : MonoBehaviour
    {
        [SerializeField]
        private RectTransform root;

        [SerializeField]
        private RectTransform racketArea;
        
        [SerializeField]
        private List<RectTransform> horizontalObjects;

        private void Update()
        {
            var rootWidth = root.rect.width;
            var horizontalWidth = horizontalObjects.Select(rt => rt.rect.width).Sum();
            var newWidth = rootWidth - horizontalWidth;
            var size = racketArea.sizeDelta;
            size.x = newWidth;
            racketArea.sizeDelta = size;
        }
    }
}