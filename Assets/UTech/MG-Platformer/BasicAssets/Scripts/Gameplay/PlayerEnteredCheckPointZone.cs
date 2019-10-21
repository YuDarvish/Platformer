using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    public class PlayerEnteredCheckPointZone : Simulation.Event<PlayerEnteredCheckPointZone>
    {
        public CheckPointZone checkPoint;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            if (!model.spawnPoint.Contains(checkPoint.transform))
            {
                model.spawnPoint.Add(checkPoint.transform);

                if (checkPoint._audio && checkPoint.CheckPointSfx)
                    checkPoint._audio.PlayOneShot(checkPoint.CheckPointSfx);
            }
        }
    }
}