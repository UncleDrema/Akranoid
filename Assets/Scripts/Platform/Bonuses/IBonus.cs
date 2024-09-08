using UnityEngine;

namespace Game.Platform.Bonuses
{
    public interface IBonus
    {
        Color BonusColor { get; }
        
        void Activate(RacketBehaviour racket);
    }
}