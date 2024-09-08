using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class DebugBonus : IBonus
    {
        public Color BonusColor => Color.magenta;

        public void Activate(RacketBehaviour racket)
        {
            Debug.Log($"Bonus collected!");
        }
    }
}