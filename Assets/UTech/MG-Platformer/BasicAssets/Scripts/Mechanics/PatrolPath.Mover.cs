using UnityEngine;

namespace Platformer.Mechanics
{
    public partial class PatrolPath
    {
        /// <summary>
        /// The Mover class oscillates between start and end points of a path at a defined speed.
        /// </summary>
        public class mMover : Mover
        {
            public mMover(PatrolPath path, float speed)
            {
                this.path = path;
                this.duration = (path.endPosition - path.startPosition).magnitude / speed;
                this.startTime = Time.time;
            }

            /// <summary>
            /// Get the position of the mover for the current frame.
            /// </summary>
            /// <value></value>
            public override Vector2 Position()
            {
                p = Mathf.InverseLerp(0, duration, Mathf.PingPong(Time.time - startTime, duration));

                return path.transform.TransformPoint(Vector2.Lerp(path.StartPosition(), path.EndPosition(), p));
            }
        }
    }
}