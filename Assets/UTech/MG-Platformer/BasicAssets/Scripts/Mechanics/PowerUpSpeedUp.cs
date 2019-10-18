using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PowerUpSpeedUp : PowerUp
    {
        [SerializeField]
        private float maxSpeed = 5f;

        public override void AffectBehavior(PlayerController player)
        {
            player.IncreaseSpeed(maxSpeed);
        }
    }
}