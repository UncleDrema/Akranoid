using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class SpeedUpBonus : IBonus
    {
        public Color BonusColor => Color.blue;
        
        public void Activate(RacketBehaviour racket)
        {
            racket.SpeedUp();
        }
    }
}