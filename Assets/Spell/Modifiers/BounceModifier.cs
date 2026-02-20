using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 * ChatGPT was used only for the ReflectVelocity function to calculate a bounce angle
 */
[CreateAssetMenu(fileName = "Bounce Modifier", menuName = "Modifiers/Bounce Modifier")]
public class BounceModifier : ModifierDefinition
{
    public int bounces = 2;
    public override void OnCast(SpellInstance instance)
    {
    }

    public override void OnDespawn(SpellInstance instance)
    {
    }

    private static Vector2 ReflectVelocity(Collider2D other, Collider2D collider, Vector2 currentVel)
    {
        ColliderDistance2D dist = other.Distance(collider);
        Vector2 normal = dist.normal;
        return Vector2.Reflect(currentVel, normal).normalized * currentVel.magnitude;
    }

    public override void OnHitLiving(SpellInstance instance, HitContext context)
    {
        Collider2D other = context.hit;
        Collider2D collider = instance.coll2D;
        if (instance.livingCollisionCount + instance.terrainCollisionCount <= bounces)
        {
            context.despawn = false;
        }
        else
        {
            context.despawn = true;
            return;
        }

        instance.rigidBody.linearVelocity = ReflectVelocity(other, collider, instance.rigidBody.linearVelocity);
    }

    public override void OnHitTerrain(SpellInstance instance, HitContext context)
    {
        Collider2D other = context.hit;
        Collider2D collider = instance.coll2D;
        if (instance.livingCollisionCount + instance.terrainCollisionCount <= bounces)
        {
            context.despawn = false;
        }
        else
        {
            context.despawn = true;
            return;
        }

        instance.rigidBody.linearVelocity = ReflectVelocity(other, collider, instance.rigidBody.linearVelocity);
    }

    public override void OnSpawn(SpellInstance instance)
    {
    }

    public override void OnUpdate(SpellInstance instance)
    {
    }
}
