using System.ComponentModel;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
[CreateAssetMenu(fileName = "Triple Shot Modifier", menuName = "Modifiers/Triple Shot Modifier")]
public class TripleShotModifier : ModifierDefinition
{
    public override void CalculateStats(SpellDefinition definition)
    {
        definition.cooldown *= 1.4f;
    }   
    public override void OnCast(SpellInstance instance)
    {
        Vector2 vel = instance.rigidBody.linearVelocity;
        float deg = -22.5f;
        for (int i = 0; i < 2; i++)
        {
            SpellInstance newSpell = GameObject.Instantiate(instance);
            newSpell.definition = instance.definition;
            Vector2 newVel = SpellHelper.Rotate(vel, deg);
            newSpell.transform.position = instance.transform.position + (Vector3)(newVel.normalized * 0.2f);
            newSpell.rigidBody.linearVelocity = newVel;
            newSpell.livingCollisionCount = instance.livingCollisionCount;
            newSpell.terrainCollisionCount = instance.terrainCollisionCount;
            deg += 45f;
        }
    }

    public override void OnDespawn(SpellInstance instance)
    {
    }
    public override void OnHitLiving(SpellInstance instance, HitContext context)
    {
    }

    public override void OnHitTerrain(SpellInstance instance, HitContext context)
    {
    }

    public override void OnSpawn(SpellInstance instance)
    {
    }

    public override void OnUpdate(SpellInstance instance)
    {
    }
}
