using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class SpeedUpBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.SpeedUp();
        }
    }
}