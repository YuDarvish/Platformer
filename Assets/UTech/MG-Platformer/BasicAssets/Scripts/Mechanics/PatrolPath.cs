using UnityEngine;

namespace Platformer.Mechanics
{
    /// <summary>
    /// This component is used to create a patrol path, two points which enemies will move between.
    /// </summary>
    public partial class PatrolPath : MovementBehaviour
    {
        /// <summary>
        /// Create a Mover instance which is used to move an entity along the path at a certain speed.
        /// </summary>
        /// <param name="speed"></param>
        /// <returns></returns>
        public override Mover CreateMover(float speed = 1) => new mMover(this, speed);

        public override Vector2 EndPosition()
        {
            return endPosition;
        }

        public override Transform GetTransform()
        {
            return transform;
        }

        public override Vector2 StartPosition()
        {
            return startPosition;
        }

        void Reset()
        {
            startPosition = Vector3.left;
            endPosition = Vector3.right;
        }
    }
}