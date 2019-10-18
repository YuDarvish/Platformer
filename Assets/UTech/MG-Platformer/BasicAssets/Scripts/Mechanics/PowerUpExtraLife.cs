using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public class PowerUpExtraLife : PowerUp
    {

        public override void AffectBehavior(PlayerController player)
        {
            player.GiveExtraLife();
        }
    }
}
