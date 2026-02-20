using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
[CreateAssetMenu(fileName = "Rapid Fire Modifier", menuName = "Modifiers/Rapid Fire Modifier")]
public class RapidFireModifier : ModifierDefinition
{
    public override void CalculateStats(SpellDefinition definition)
    {
        definition.size *= 0.8f;
        definition.speed *= 1.2f;
        definition.cooldown *= 0.5f;
        definition.multiplier *= 0.8f;
    }
    public override void OnCast(SpellInstance instance)
    {
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
