using UnityEngine;

public class HobgoblinEnemyController : EnemyController
{
    public EnemyData data;
    public MeleeData melee;

    void Start()
    {
        melee.attackBox.enabled = false;
        SwitchState(new MeleeChaseState(data, melee, this));
    }

    public override void ControllerUpdate()
    {
        if (Mathf.Abs(data.rigidBody.linearVelocity.x) >= 0.4f || Mathf.Abs(data.rigidBody.linearVelocity.y) >= 0.4f)
        {
            data.spriteAnimator.SetBool("isWalking", true);
        }
        else
        {
            data.spriteAnimator.SetBool("isWalking", false);
        }
        if (data.rigidBody.linearVelocity.x > 0)
        {
            data.spriteRenderer.flipX = false;
            melee.attackBox.transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            data.spriteRenderer.flipX = true;
            melee.attackBox.transform.localScale = new Vector3(-1, 1, 1);
        }
    }
}
