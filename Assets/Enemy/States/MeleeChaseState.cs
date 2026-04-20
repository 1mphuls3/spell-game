using UnityEngine;

public class MeleeChaseState : EnemyBehaviorState
{
    private MeleeData melee;
    public MeleeChaseState(EnemyData data, MeleeData melee, EnemyController controller)
        : base(data, controller)
    {
        this.melee = melee;
    }

    public override void Update()
    {
        data.rigidBody.linearVelocity = Vector2.MoveTowards(data.rigidBody.linearVelocity, data.directionToPlayer * data.moveSpeed, data.acceleration * Time.deltaTime);

        if (data.distanceToPlayer <= data.attackRange)
        {
            controller.SwitchState(new MeleeAttackState(data, melee, controller));
        }
    }
}
