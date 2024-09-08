using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class ScaleUpBonus : IBonus
    {
        public Color BonusColor => Color.gray;
        
        public void Activate(RacketBehaviour racket)
        {
            racket.ScaleUp();
        }
    }
}