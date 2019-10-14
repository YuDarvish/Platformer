using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Core.Simulation;
using Platformer.Gameplay;
using Platformer.Model;
using Platformer.Core;

namespace Platformer.Mechanics
{
    /// <summary>
    /// CheckPointZone components mark a collider which will schedule a
    /// PlayerEnteredDeathZone event when the player enters the trigger.
    /// </summary>
    /// 
    public class CheckPointZone : MonoBehaviour
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        void OnTriggerEnter2D(Collider2D collider)
        {
            if(!model.spawnPoint.Contains(transform))
                model.spawnPoint.Add(transform);
        }
    }
}
