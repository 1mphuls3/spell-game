using System.Collections.Generic;
using UnityEngine;

public class SpellData : MonoBehaviour
{
    public GameObject spell;
    public List<ModifierDefinition> modifiers;

    public float spellCooldown = 2f;
    public float spellDamage = 2f;
    public float spellSpeed = 2f;
    public float spellSize = 0.2f;
    public float spellRange = 1f;
    public Texture2D color;
    public Vector2 shootPos;
}
