using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class DuplicateBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.DuplicateBalls();
        }
    }
}