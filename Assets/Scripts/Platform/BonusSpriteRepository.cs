using UnityEngine;

namespace Game.Platform
{
    [CreateAssetMenu(fileName = nameof(BonusSpriteRepository), menuName = nameof(BonusSpriteRepository))]
    public class BonusSpriteRepository : ScriptableObject
    {
        public Sprite DuplicateSprite;
        
        public Sprite HealSprite;
        
        public Sprite GlueSprite;
        
        public Sprite ScaleDownSprite;
        
        public Sprite ScaleUpSprite;
        
        public Sprite SpeedDownSprite;
        
        public Sprite SpeedUpSprite;
    }
}