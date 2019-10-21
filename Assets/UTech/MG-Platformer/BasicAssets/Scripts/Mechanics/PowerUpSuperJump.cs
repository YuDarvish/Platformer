using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PowerUpSuperJump : PowerUp
    {
        [SerializeField]
        private float jumpTakeOffSpeed = 5f;
               
        public override void AffectBehavior(PlayerController player)
        {
            player.IncreaseJump(jumpTakeOffSpeed);
        }
    }
}