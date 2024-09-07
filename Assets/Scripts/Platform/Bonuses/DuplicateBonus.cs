namespace Game.Platform.Bonuses
{
    public class DuplicateBonus : BonusBase
    {
        public override void Activate(RacketBehaviour racket)
        {
            racket.DuplicateBalls();
        }
    }
}