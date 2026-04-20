using System.Collections;
using UnityEngine;

public class MeleeAttackState : EnemyBehaviorState
{
    Vector2 lungeDir;
    bool lunging;
    private MeleeData melee;

    public MeleeAttackState(EnemyData data, MeleeData melee, EnemyController controller)
        : base(data, controller)
    {
        this.melee = melee;
    }

    public override void Enter()
    {
        lungeDir = data.directionToPlayer;
        lunging = true;
    }

    public override void Update()
    {
        data.spriteAnimator.SetBool("isLunging", true);
        if (lunging)
        {
            data.rigidBody.linearVelocity = lungeDir * melee.speed * 3f;

            controller.StartCoroutine(Lunge());
        }
    }

    private IEnumerator Lunge()
    {
        yield return new WaitForSeconds(0.25f);
        melee.attackBox.enabled = true;
        yield return new WaitForSeconds(0.15f);
        melee.attackBox.enabled = false;
        yield return new WaitForSeconds(0.35f);
        lunging = false;
        data.spriteAnimator.SetBool("isLunging", false);
        controller.SwitchState(new MeleeCooldownState(data, melee, controller));
    }
}