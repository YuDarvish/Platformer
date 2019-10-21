using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public abstract class PowerUp : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D other)
        {
           if(other.tag == "Player")
            {
                AffectBehavior(other.GetComponent<PlayerController>());
                RecycleMe();
            }
        }

        public abstract void AffectBehavior(PlayerController player);

        public void RecycleMe()
        {
            PowerUpSpawn.instance.PushOnStack(this);
        }
    }
}