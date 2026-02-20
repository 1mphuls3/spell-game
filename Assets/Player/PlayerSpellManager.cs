using System.Collections.Generic;
using System.Drawing;
using Unity.VisualScripting;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class PlayerSpellManager : MonoBehaviour
{
    [SerializeField] public List<ModifierDefinition> modifiers;
    [SerializeField] public PlayerController player;

    public void Equip(ModifierDefinition definition)
    {
        modifiers.Add(definition);
        // Sort the modifiers list by their priority
        modifiers.Sort((a, b) => a.priority.CompareTo(b.priority));

        SpellDefinition spell = new SpellDefinition(player.spellDamage, player.spellSpeed, player.spellCooldown, 1f, player.spellSize,
            player.spellRange, modifiers, player.color);
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.CalculateStats(spell);
        }

        spell.ComputeDamage();
        player.definition = spell;
    }

    public void Unequip(ModifierDefinition definition)
    {
        modifiers.Remove(definition);
        // Sort the modifiers list by their priority
        modifiers.Sort((a, b) => a.priority.CompareTo(b.priority));
    }
}
