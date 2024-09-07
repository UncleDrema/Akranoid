namespace Game.Platform.Bonuses
{
    public class GlueBonus : BonusBase
    {
        public override void Activate(RacketBehaviour racket)
        {
            racket.CoverWithGlue(5f);
        }
    }
}