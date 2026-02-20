using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class HitContext
{
    public Collider2D hit;
    public bool despawn;
    public HitContext(Collider2D hit)
    {
        this.hit = hit;
        this.despawn = true;
    }
}
