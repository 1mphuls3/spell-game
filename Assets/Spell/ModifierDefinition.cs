using UnityEngine;

public abstract class ModifierDefinition
{
    // Modify the behavior of a SpellInstance once when it is cast
    public abstract void OnCast(SpellInstance instance);
    // Modify the behavior of a SpellInstance once when it is created
    public abstract void OnSpawn(SpellInstance instance);
    // Modify the behavior of SpellInstance continuously every frame
    public abstract void OnUpdate(SpellInstance instance);
    // Modify the behavior of a Spellinstance on collision with an enemy or terrain
    //  Return true if the SpellInstance should be destroyed after the collision
    public abstract bool OnHitEnemy(SpellInstance instance, HitContext context);
    public abstract bool OnHitTerrain(SpellInstance instance, HitContext context);
    // Modify the behavior of a SpellInstance just before it is destroyed
    public abstract void OnDespawn(SpellInstance instance);
}
