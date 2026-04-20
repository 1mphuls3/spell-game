using UnityEngine;

public class ViperEnemyController : EnemyController
{
    public EnemyData data;
    public SpellData spell;

    void Start()
    {   
        SwitchState(new RangedChaseState(data, spell, this));
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
        }
        else
        {
            data.spriteRenderer.flipX = true;
        }
    }
}
