using UnityEngine;

public class SpellDefinition
{
    public float damage = 2f;
    public float speed = 1f;
    public float cooldown = 0.5f;
    public float cost = 1f;
    public float size = 1f;

    [SerializeField] public ModifierDefinition[] modifiers;

    public SpellDefinition(float damage, float speed, float cooldown, float cost, float size, ModifierDefinition[] modifiers)
    {
        this.damage = damage;
        this.speed = speed;
        this.cooldown = cooldown;
        this.cost = cost;
        this.size = size;
        this.modifiers = modifiers;
    }

    // Called once when player attempts to cast a spell, before the spell projectile is instantiated
    public void Cast(CastContext context)
    {
        SpellInstance instance = GameObject.Instantiate(new SpellInstance(context, this));
        instance.gameObject.transform.position = context.startPos;

        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnCast(instance);
        }
    }
}
public class CastContext
{
    public GameObject caster;
    public Vector2 startPos;
    public Vector2 direction;

    public CastContext(GameObject caster, Vector2 startPos, Vector2 direction)
    {
        this.caster = caster;
        this.startPos = startPos;
        this.direction = direction;
    }
}

public enum CollisionType
{
    Enemy,
    Terrain
}

public class HitContext
{
    public Collider2D hit;
    public CollisionType type;
    public bool despawn;

    public HitContext(Collider2D hit, CollisionType type)
    {
        this.hit = hit;
        this.type = type;
        this.despawn = true;
    }
}
