using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class ScaleUpBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.ScaleUp();
        }
    }
}