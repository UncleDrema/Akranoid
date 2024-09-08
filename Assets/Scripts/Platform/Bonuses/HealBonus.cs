using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class HealBonus : IBonus
    {
        public Color BonusColor => Color.red;

        public void Activate(RacketBehaviour racket)
        {
            racket.AddHealth();
        }
    }
}