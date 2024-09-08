using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class HealBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.AddHealth();
        }
    }
}