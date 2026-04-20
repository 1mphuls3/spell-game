using System.Collections;
using UnityEngine;

public class RangedAttackState : EnemyBehaviorState
{
    float shootTimer;

    int strafeDir = 1;
    float strafeTimer;
    SpellData spell;

    public RangedAttackState(EnemyData data, SpellData  spell, EnemyController controller)
        : base(data, controller)
    {
        this.spell = spell;
    }

    public override void Enter()
    {
        shootTimer = spell.spellCooldown;
        strafeTimer = 2f;
    }

    public override void Update()
    {
        shootTimer -= Time.deltaTime;
        strafeTimer -= Time.deltaTime;

        Vector2 toPlayer = data.directionToPlayer;
        Vector2 perpendicular = new Vector2(-toPlayer.y, toPlayer.x);

        //data.rigidBody.linearVelocity = perpendicular * strafeDir * data.moveSpeed;

        data.rigidBody.linearVelocity = Vector2.MoveTowards(data.rigidBody.linearVelocity, perpendicular * strafeDir * data.moveSpeed, data.acceleration * Time.deltaTime);

        if (strafeTimer <= 0)
        {
            strafeTimer = Random.Range(1f, 3f);
            strafeDir *= -1;
        }

        if (shootTimer <= 0)
        {
            controller.StartCoroutine(Shoot());
            shootTimer = spell.spellCooldown + Random.Range(-0.2f, 0.2f);
        }

        if (data.distanceToPlayer > data.attackRange + 2f)
        {
            controller.SwitchState(new RangedChaseState(data, spell, controller));
        }
    }

    private IEnumerator Shoot()
    {
        data.spriteAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.417f);
        Vector2 direction = (data.player.position - data.rigidBody.transform.position).normalized;
        Vector2 targetVel = direction;
        Vector2 position = data.spriteRenderer.flipX ? (Vector2)data.rigidBody.transform.position - spell.shootPos : (Vector2)data.rigidBody.transform.position + spell.shootPos;

        SpellDefinition definition = new SpellDefinition(spell.spellDamage, spell.spellSpeed, 0.2f, 1f, spell.spellSize, spell.spellRange, spell.modifiers, spell.color);
        CastContext context = new CastContext(controller.gameObject, position, targetVel * definition.speed);
        definition.spell = spell.spell.GetComponent<SpellInstance>();

        definition.Cast(context);
        yield return new WaitForSeconds(0.416f);
        data.spriteAnimator.SetBool("isAttacking", false);
    }
}
