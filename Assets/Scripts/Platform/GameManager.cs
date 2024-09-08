using System;
using System.Collections.Generic;
using Game.Platform.Bonuses;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.Platform
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] 
        private RectTransform root;

        [SerializeField]
        private LevelUi levelUi;
        
        [SerializeField]
        private FinishUi finishUi;

        [SerializeField]
        private BonusBehaviour bonusPrefab;

        [SerializeField]
        private RacketBehaviour racket;

        public static RacketBehaviour Racket => Instance.racket;
        
        public static GameManager Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        public static void WinLevel()
        {
            Instance.finishUi.WinLevel();
        }
        
        public static void FailLevel()
        {
            Instance.finishUi.FailLevel();
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

        public static void SpawnBonus(Vector3 position)
        {
            var bonus = InstantiateObject(Instance.bonusPrefab, position);
            bonus.SetBonus(GetRandomBonus());
        }

        private static IBonus GetRandomBonus()
        {
            var bonusList = new List<IBonus>();

            if (FindObjectsOfType<BallBehaviour>().Length < 64)
            {
                bonusList.Add(new DuplicateBonus());
            }

            if (Racket.CanScaleUp)
            {
                bonusList.Add(new ScaleUpBonus());
            }
            
            if (Racket.CanScaleDown)
            {
                bonusList.Add(new ScaleDownBonus());
            }
            
            if (Racket.CanSpeedUp)
            {
                bonusList.Add(new SpeedUpBonus());
            }
                        
            if (Racket.CanSpeedDown)
            {
                bonusList.Add(new SpeedDownBonus());
            }

            if (Racket.GlueTimer < 30f)
            {
                bonusList.Add(new GlueBonus());
            }

            if (Racket.Health < 5)
            {
                bonusList.Add(new HealBonus());
            }
            
            return bonusList[Random.Range(0, bonusList.Count)];
        }
    }
}