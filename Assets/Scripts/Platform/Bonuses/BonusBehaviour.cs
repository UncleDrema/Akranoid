using UnityEngine;
using UnityEngine.UI;

namespace Game.Platform.Bonuses
{
    public class BonusBehaviour : MonoBehaviour
    {
        [SerializeField]
        private Image image;
        
        private IBonus _bonus;

        public void SetBonus(IBonus bonus)
        {
            _bonus = bonus;
            image.color = bonus.BonusColor;
        }
        
        public void Use(RacketBehaviour racket)
        {
            _bonus?.Activate(racket);
            Destroy(gameObject);
        }
    }
}