using UnityEngine;

namespace Game.Platform.Bonuses
{
    public interface IBonus
    {
        void Activate(RacketBehaviour racket);
    }
}