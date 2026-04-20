using UnityEngine;
using UnityEngine.InputSystem.XR;

public class RangedChaseState : EnemyBehaviorState
{
    SpellData spell;
    public RangedChaseState(EnemyData data, SpellData spell, EnemyController controller)
        : base(data, controller)
    {
        this.spell = spell;
    }

    public override void Update()
    {
        Vector2 move = data.directionToPlayer;

        data.rigidBody.linearVelocity = move * data.moveSpeed;

        if (data.distanceToPlayer <= data.attackRange)
        {
            controller.SwitchState(new RangedAttackState(data, spell, controller));
        }
    }
}
