using System.Collections;
using System.Collections.Generic;
using Platformer.Mechanics;
using UnityEngine;

namespace Platformer.Mechanics
{
    public partial class ChasePath : MovementBehaviour
    {
        public GameObject targetPosition;

        public override Mover CreateMover(float speed = 1) => new mMover(this, speed * Time.deltaTime);

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

        public Vector2 TargetPosition()
        {
            return targetPosition.transform.position;
        }

        public void Update()
        {
            endPosition = TargetPosition();
            startPosition = transform.position;
        }
    }
}