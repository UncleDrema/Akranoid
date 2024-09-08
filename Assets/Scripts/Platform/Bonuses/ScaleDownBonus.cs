using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class ScaleDownBonus : IBonus
    {
        public Color BonusColor => Color.cyan;
        
        public void Activate(RacketBehaviour racket)
        {
            racket.ScaleDown();
        }
    }
}