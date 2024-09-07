using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class DebugBonus : BonusBase
    {
        public override void Activate(RacketBehaviour racket)
        {
            Debug.Log($"Bonus collected!");
        }
    }
}