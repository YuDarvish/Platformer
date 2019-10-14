using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static Platformer.Mechanics.PatrolPath;

public abstract class MovementBehaviour : MonoBehaviour
{
    public Vector2 startPosition, endPosition;

    public abstract Mover CreateMover(float speed);
    public abstract Vector2 EndPosition();
    public abstract Vector2 StartPosition();
    public abstract Transform GetTransform();
}
