using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class DebugBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            Debug.Log($"Bonus collected!");
        }
    }
}