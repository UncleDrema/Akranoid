using System;
using System.Collections.Generic;
using TriInspector;
using UnityEngine;

namespace Game.Menu
{
    [CreateAssetMenu(fileName = nameof(LevelRepository), menuName = nameof(LevelRepository))]
    public class LevelRepository : ScriptableObject
    {
        public List<LevelEntry> Levels;
        
        [Serializable]
        public class LevelEntry
        {
            [Scene]
            public string Scene;

            public Sprite Preview;
        }
    }
}