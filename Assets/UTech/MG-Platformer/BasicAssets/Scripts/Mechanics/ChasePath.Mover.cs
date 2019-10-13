using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer.Mechanics
{
    public partial class ChasePath
    {
        public class mMover : Mover
        {
            public mMover(ChasePath path, float speed)
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
                return path.transform.TransformPoint(path.EndPosition());
            }
        }
    }
}