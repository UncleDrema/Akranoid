using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class GlueBonus : IBonus
    {
        public void Activate(RacketBehaviour racket)
        {
            racket.CoverWithGlue(10f);
        }
    }
}