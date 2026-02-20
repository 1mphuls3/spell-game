using System.Collections.Generic;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class SpellInstance : MonoBehaviour
{
    public Collider2D coll2D;
    public Rigidbody2D rigidBody;
    public SpriteRenderer spriteRenderer;

    public SpellDefinition definition;
    public List<ModifierDefinition> modifiers;
    public GameObject caster;
    
    // Stored count of the number of enemy and terrain collisions the projectile has experienced
    public int terrainCollisionCount = 0;
    public int livingCollisionCount = 0;

    public float age = 0f;

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
        age += Time.deltaTime;
        if(age > definition.range)
        {
            OnDespawn();
        }
    }

    // Called when the projectile collides with a living entity such as a player or enemy
    public void OnHitLiving(HitContext context)
    {
        GameObject entity = context.hit.gameObject;
        Health health = entity.GetComponent<Health>();
        livingCollisionCount++;
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnHitLiving(this, context);
        }

        health.Damage(definition.finalDamage);

        if (context.despawn && age > 0.2f)
        {
            OnDespawn();
        }
    }

    // Called when the projectile collides with terrain such as walls or another nonliving object
    public void OnHitTerrain(HitContext context)
    {
        terrainCollisionCount++;
        foreach (ModifierDefinition modifier in modifiers)
        {
            modifier.OnHitTerrain(this, context);
        }

        if (context.despawn && age > 0.2f)
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
        age = 0f;
        coll2D = this.gameObject.GetComponent<Collider2D>();
        spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        rigidBody = this.gameObject.GetComponent<Rigidbody2D>();

        this.OnSpawn();
    }

    private void Update()
    {
        this.OnUpdate();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (caster.gameObject.CompareTag("Player"))
        {
            // If the collision is with an object tagged as an enemy, call OnImpact with the Enemy impact mode
            if (other.gameObject.CompareTag("Enemy"))
            {
                OnHitLiving(new HitContext(other));
                return;
            }
        }
        else
        {
            // If the collision is with an object tagged as a player, call OnImpact with the Enemy impact mode
            if (other.gameObject.CompareTag("Player"))
            {
                OnHitLiving(new HitContext(other));
                return;
            }
        }
        // If the collision is with an object tagged as terrain, call OnImpact with the Terrain impact mode
        if (other.gameObject.CompareTag("Terrain"))
        {
            OnHitTerrain(new HitContext(other));
            return;
        }
    }
}
