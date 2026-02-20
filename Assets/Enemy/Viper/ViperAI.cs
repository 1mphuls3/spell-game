using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class ViperAI : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Animator spriteAnimator;
    [SerializeField] private SpriteRenderer spriteRenderer;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 80f;
    [SerializeField] private float distance = 6f;

    // Number of seconds between each attack
    [SerializeField] private float attackCooldown = 2f;
    // Random variation in seconds between each attack
    private float attackRandom = 0f;

    [SerializeField] private float spellDamage = 2f;
    [SerializeField] private float spellSpeed = 2f;
    [SerializeField] private float spellSize = 0.2f;
    [SerializeField] private float spellRange = 1f;
    [SerializeField] private Texture2D color;

    // Spell origin offset
    [SerializeField] private Vector2 shootPos;

    [SerializeField] private GameObject spell;
    [SerializeField] private List<ModifierDefinition> modifiers;

    private GameObject targetPlayer;
    private float attackTimer;
    void Start()
    {
        // Get the player
        targetPlayer = GameObject.FindGameObjectWithTag("Player");
    }

    void Update()
    {
        // Get the direction from this enemy to the target player and multiply it by the enemy's move speed
        Vector2 direction = (targetPlayer.transform.position - this.transform.position).normalized;
        Vector2 targetVel = direction * moveSpeed;

        // Only chase and attack if the player is within range
        if (Vector2.Distance(targetPlayer.transform.position, this.transform.position) < distance)
        {
            rigidBody.linearVelocity = Vector2.MoveTowards(rigidBody.linearVelocity, targetVel, acceleration * Time.deltaTime);

            attackTimer += Time.deltaTime;

            if (attackTimer >= attackCooldown + attackRandom)
            {
                StartCoroutine(Attack());
                attackTimer = 0f;
            }
        }
        else attackTimer = 0f;

        // If the velocity is >= 0.4 in any direction,enable walking animation
        if (Mathf.Abs(rigidBody.linearVelocity.x) >= 0.4f || Mathf.Abs(rigidBody.linearVelocity.y) >= 0.4f)
        {
            spriteAnimator.SetBool("isWalking", true);
        }
        else
        {
            spriteAnimator.SetBool("isWalking", false);
        }

        // If not moving right flip the sprite to face left
        if (rigidBody.linearVelocity.x > 0)
        {
            spriteRenderer.flipX = false;
        } 
        else
        {
            spriteRenderer.flipX = true;
        }
    }

    private IEnumerator Attack()
    {
        spriteAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.417f);
        Vector2 direction = (targetPlayer.transform.position - this.transform.position).normalized;
        Vector2 targetVel = direction;
        Vector2 position = spriteRenderer.flipX ? (Vector2)transform.position - shootPos: (Vector2)transform.position + shootPos;

        SpellDefinition definition = new SpellDefinition(spellDamage, spellSpeed, 0.2f, 1f, spellSize, spellRange, modifiers, color);
        CastContext context = new CastContext(gameObject, position, targetVel * definition.speed);
        definition.spell = spell.GetComponent<SpellInstance>();

        definition.Cast(context);
        yield return new WaitForSeconds(0.416f);
        spriteAnimator.SetBool("isAttacking", false);
        attackRandom = Random.Range(-0.2f, 0.2f);
    }
}
