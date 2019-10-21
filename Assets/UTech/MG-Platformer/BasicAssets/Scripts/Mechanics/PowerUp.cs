using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    public abstract class PowerUp : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
           if(other.tag == "Player")
           {
                var ev = Schedule<PlayerLoadPowerUp>();
                ev.powerUp = this;
            }
        }

        public abstract void AffectBehavior(PlayerController player);

        public void RecycleMe()
        {
            PowerUpSpawn.instance.PushOnStack(this);
        }
    }
}