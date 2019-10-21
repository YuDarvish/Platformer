using System.Collections;
using System.Collections.Generic;
using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;

namespace Platformer.Mechanics
{
    public partial class ChasePath : MovementBehaviour
    {
        public LayerMask layerMask;
        RaycastHit2D leftLine;
        RaycastHit2D rightLine;
        RaycastHit2D upLine;

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();
        Vector2 defaultDirection = new Vector2(150f, 0f);
        
        public void Update()
        {
            leftLine = Physics2D.Raycast(transform.position, Vector2.left, 5f, layerMask);
            rightLine = Physics2D.Raycast(transform.position, Vector2.right, 5f, layerMask);
            upLine = Physics2D.Raycast(transform.position, Vector2.up, 10f, layerMask);

            if (leftLine.collider != null && model.player.health.IsAlive)
            {
                endPosition = leftLine.point;
            }
            else if(rightLine.collider != null && model.player.health.IsAlive)
            {
                endPosition = rightLine.point;
            }
            else if (upLine.collider != null && model.player.health.IsAlive)
            {
                endPosition = defaultDirection;
            }
            else
            {
                endPosition = transform.position;
            }
        }

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
    }
}