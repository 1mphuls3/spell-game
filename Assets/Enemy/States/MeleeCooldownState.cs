using UnityEngine;

public class MeleeCooldownState : EnemyBehaviorState
{
    private MeleeData melee;

    float timer = 1f;

    public MeleeCooldownState(EnemyData data, MeleeData melee, EnemyController controller)
        : base(data, controller)
    {
        this.melee = melee;
    }

    public override void Enter()
    {
        timer = melee.cooldown + Random.Range(0, 0.5f);
    }

    public override void Update()
    {
        data.rigidBody.linearVelocity = Vector2.zero;

        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            controller.SwitchState(new MeleeChaseState(data, melee, controller));
        }
    }
}
