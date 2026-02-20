using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class CastContext
{
    public GameObject caster;
    public Vector2 startPos;
    public Vector2 direction;

    public CastContext(GameObject caster, Vector2 startPos, Vector2 direction)
    {
        this.caster = caster;
        this.startPos = startPos;
        this.direction = direction;
    }
}
