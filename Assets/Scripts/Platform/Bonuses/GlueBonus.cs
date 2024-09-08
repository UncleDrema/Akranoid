using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class GlueBonus : IBonus
    {
        public Color BonusColor => Color.yellow;

        public void Activate(RacketBehaviour racket)
        {
            racket.CoverWithGlue(5f);
        }
    }
}