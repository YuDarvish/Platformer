using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;

namespace Platformer.Gameplay
{
    public class PlayerLoadPowerUp : Simulation.Event<PlayerLoadPowerUp>
    {
        public PowerUp powerUp;
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            powerUp.AffectBehavior(model.player);
            powerUp.RecycleMe();
        }
    }
}