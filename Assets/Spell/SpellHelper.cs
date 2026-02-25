using UnityEngine;

/*
 * ChatGPT was referenced for the ReflectVelocity and Rotate methods
 */
public class SpellHelper
{
    public static Vector2 Rotate(Vector2 vec, float deg)
    {
        Quaternion rotation = Quaternion.AngleAxis(deg, Vector3.forward);

        return rotation * vec;
    }

    public static Vector2 ReflectVelocity(Collider2D other, Collider2D collider, Vector2 currentVel)
    {
        ColliderDistance2D dist = other.Distance(collider);
        Vector2 normal = dist.normal;
        return Vector2.Reflect(currentVel, normal).normalized * currentVel.magnitude;
    }
}
