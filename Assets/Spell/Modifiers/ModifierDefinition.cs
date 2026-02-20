using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class ModifierDefinition : ScriptableObject
{
    public Sprite icon;
    public int priority;
    // Calculate the base stats of the spell before it is cast
    public virtual void CalculateStats(SpellDefinition definition)
    {
    }
    // Modify the behavior of a SpellInstance once as it is cast
    public virtual void OnCast(SpellInstance instance)
    {

    }
    // Modify the behavior of a SpellInstance once when it is created
    public virtual void OnSpawn(SpellInstance instance)
    {

    }
    // Modify the behavior of SpellInstance continuously every frame
    public virtual void OnUpdate(SpellInstance instance)
    {

    }
    // Modify the behavior of a Spellinstance on collision with an enemy or terrain
    //  Return true if the SpellInstance should be destroyed after the collision
    public virtual void OnHitLiving(SpellInstance instance, HitContext context)
    {

    }
    public virtual void OnHitTerrain(SpellInstance instance, HitContext context)
    {

    }
    // Modify the behavior of a SpellInstance just before it is destroyed
    public virtual void OnDespawn(SpellInstance instance)
    {

    }
}
