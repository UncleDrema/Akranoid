using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class SpeedDownBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.SpeedDown();
        }
    }
}