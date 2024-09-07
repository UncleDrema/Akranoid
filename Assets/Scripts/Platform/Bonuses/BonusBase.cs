using UnityEngine;

namespace Game.Platform.Bonuses
{
    public abstract class BonusBase : MonoBehaviour
    {
        public abstract void Activate(RacketBehaviour racket);

        public void Use(RacketBehaviour racket)
        {
            Activate(racket);
            Destroy(gameObject);
        }
    }
}