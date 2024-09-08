using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class SpeedDownBonus : IBonus
    {
        public Color BonusColor => Color.black;
        
        public void Activate(RacketBehaviour racket)
        {
            racket.SpeedDown();
        }
    }
}