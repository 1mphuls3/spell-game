using System.Collections.Generic;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class SpellDefinition
{
    public float damage, speed, cooldown, cost, size, range;

    public List<ModifierDefinition> modifiers;
    public SpellInstance spell;
    public Texture2D color;

    // Damage calcluation 
    public float addition;
    public float multiplier = 1f;
    public float finalDamage;
    public SpellDefinition(float damage, float speed, float cooldown, float cost, float size, float range, List<ModifierDefinition> modifiers, Texture2D color)
    {
        this.damage = damage;
        this.speed = speed;
        this.cooldown = cooldown;
        this.cost = cost;
        this.size = size;
        this.range = range;
        this.modifiers = modifiers;
        this.color = color;
    }

    // Called once when player attempts to cast a spell, when the spell projectile is instantiated
    public void Cast(CastContext context)
    {
        ComputeDamage();
        SpellInstance instance = GameObject.Instantiate(spell);
        instance.definition = this;
        instance.modifiers = modifiers;
        instance.caster = context.caster;

        instance.transform.localScale = new Vector3(size, size, 1);
        instance.gameObject.transform.position = context.startPos;
        instance.rigidBody.linearVelocity = context.direction * speed;
        instance.spriteRenderer.material.SetTexture("_ColorRamp", color);

        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnCast(instance);
        }
    }

    public void ComputeDamage()
    {
        finalDamage = (damage + addition) * multiplier;
    }
}
