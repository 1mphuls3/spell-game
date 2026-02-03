using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

public class SpellInstance : MonoBehaviour
{
    public Collider2D coll2D;

    public float damage = 2f;
    public float speed = 1f;
    public float cooldown = 0.5f;
    public float cost = 1f;
    public float size = 1f;

    public ModifierDefinition[] modifiers;
    public CastContext context;

    public SpellInstance(CastContext context, SpellDefinition definition)
    {
        this.damage = definition.damage;
        this.speed = definition.speed;
        this.cost = definition.cost;
        this.size = definition.size;

        this.context = context;
        this.modifiers = definition.modifiers;
    }

    // True if the projectile should be destroyed on collision
    public bool despawnOnHit = true;
    // Stored count of the number of enemy and terrain collisions the projectile has experienced
    public int terrainCollisionCount = 0;
    public int enemyCollisionCount = 0;

    // Called once the spell projectile is instantiated
    public void OnSpawn()
    {
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnSpawn(this);
        }
    }

    // Called each frame during the projectile's lifetime
    //  Used to modify projectile movement or similar continuous effects
    public void OnUpdate()
    {
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnUpdate(this);
        }
    }

    // Called when the projectile collides with something
    // Impact mode:
    //  Enemy = enemy collision
    //  Terrain = wall/obstacle collision
    public void OnHitEnemy(HitContext context)
    {
        enemyCollisionCount++;
        foreach (ModifierDefinition modifier in modifiers)
        {
            bool shouldDestroy = modifier.OnHitEnemy(this, context);
        }
        if (context.despawn)
        {
            OnDespawn();
        }
    }

    public void OnHitTerrain(HitContext context)
    {
        terrainCollisionCount++;
        foreach (ModifierDefinition modifier in modifiers)
        {
            bool shouldDestroy = modifier.OnHitTerrain(this, context);
        }
        if (context.despawn)
        {
            OnDespawn();
        }
    }


    // Called before the projectile is destroyed
    public void OnDespawn()
    {
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnDespawn(this);
        }
        GameObject.Destroy(gameObject);
    }

    private void Start()
    {
        this.OnSpawn();
    }

    private void Update()
    {
        this.OnUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // If the collision is with an object tagged as an enemy, call OnImpact with the Enemy impact mode
        if(collision.gameObject.CompareTag("Enemy"))
        {
            OnHitEnemy(new HitContext(collision.collider, CollisionType.Enemy));
        }
        // If the collision is with an object tagged as terrain, call OnImpact with the Terrain impact mode
        else if (collision.gameObject.CompareTag("Terrain"))
        {
            OnHitTerrain(new HitContext(collision.collider, CollisionType.Terrain));
        }
    }
}
