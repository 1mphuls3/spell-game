using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
[CreateAssetMenu(fileName = "Heavy Shot Modifier", menuName = "Modifiers/Heavy Shot Modifier")]
public class HeavyShotModifier : ModifierDefinition
{
    public override void CalculateStats(SpellDefinition definition)
    {
        definition.size *= 1.5f;
        definition.speed *= 0.8f;
        definition.cooldown *= 1.5f;
        definition.multiplier *= 1.5f;
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
