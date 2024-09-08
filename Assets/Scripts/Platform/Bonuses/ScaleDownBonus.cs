using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class ScaleDownBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.ScaleDown();
        }
    }
}