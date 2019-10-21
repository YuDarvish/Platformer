using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover
{
    protected MovementBehaviour path;
    protected float p = 0;
    protected float duration;
    protected float startTime;
        
    public abstract Vector2 Position();
}
