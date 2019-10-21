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
            }

            /// <summary>
            /// Get the position of the mover for the current frame.
            /// </summary>
            /// <value></value>
            public override Vector2 Position()
            {
                return path.endPosition;
            }
        }
    }
}