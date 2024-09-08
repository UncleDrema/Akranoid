﻿using UnityEngine;

namespace Game.Platform.Bonuses
{
    public class DuplicateBonus : IBonus
    {
        public Color BonusColor => Color.green;

        public void Activate(RacketBehaviour racket)
        {
            racket.DuplicateBalls();
        }
    }
}