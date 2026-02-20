using System.ComponentModel;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 * ChatGPT was used only for the Rotate function
 */
[CreateAssetMenu(fileName = "Split Modifier", menuName = "Modifiers/Split Modifier")]
public class SplitModifier : ModifierDefinition
{
    public int splitCount = 2;

    public override void OnCast(SpellInstance instance)
    {
    }

    public override void OnDespawn(SpellInstance instance)
    {
    }

    public static Vector2 Rotate(Vector2 vec, float deg)
    {
        float rad = Mathf.Rad2Deg * deg;
        float sin = Mathf.Sin(rad);
        float cos = Mathf.Cos(rad);

        return new Vector2(vec.x * cos - vec.y * sin, vec.x * sin + vec.y * cos).normalized * vec.magnitude;
    }

    public override void OnHitLiving(SpellInstance instance, HitContext context)
    {
        Collider2D other = context.hit;
        Collider2D collider = instance.coll2D;
        Vector2 vel = instance.rigidBody.linearVelocity;
        if (instance.livingCollisionCount + instance.terrainCollisionCount == 1)
        {
            context.despawn = true;
        }
        else if (instance.livingCollisionCount + instance.terrainCollisionCount > 1)
        {
            context.despawn = true;
            return;
        }

        float deg = -22.5f;
        for(int i = 0; i < splitCount; i++)
        {
            SpellInstance newSpell = GameObject.Instantiate(instance);
            newSpell.definition = instance.definition;
            Vector2 newVel = Rotate(vel, deg);
            newSpell.transform.position = instance.transform.position + (Vector3)(newVel.normalized * 0.2f);
            newSpell.rigidBody.linearVelocity = newVel;
            newSpell.livingCollisionCount = instance.livingCollisionCount;
            newSpell.terrainCollisionCount = instance.terrainCollisionCount;
            deg += 22.5f;
        }
    }

    public override void OnHitTerrain(SpellInstance instance, HitContext context)
    {
        Collider2D other = context.hit;
        Collider2D collider = instance.coll2D;
        Vector2 vel = instance.rigidBody.linearVelocity;
        if (instance.livingCollisionCount + instance.terrainCollisionCount == 1)
        {
            context.despawn = true;
        }
        else if (instance.livingCollisionCount + instance.terrainCollisionCount > 1)
        {
            context.despawn = true;
            return;
        }

        float deg = -22.5f;
        for (int i = 0; i < splitCount; i++)
        {
            SpellInstance newSpell = GameObject.Instantiate(instance);
            newSpell.definition = instance.definition;
            Vector2 newVel = Rotate(vel, deg);
            newSpell.transform.position = instance.transform.position + (Vector3)(newVel.normalized * 0.2f);
            newSpell.rigidBody.linearVelocity = newVel;
            newSpell.livingCollisionCount = instance.livingCollisionCount;
            newSpell.terrainCollisionCount = instance.terrainCollisionCount;
            deg += 22.5f;
        }
    }

    public override void OnSpawn(SpellInstance instance)
    {
    }

    public override void OnUpdate(SpellInstance instance)
    {
    }
}
