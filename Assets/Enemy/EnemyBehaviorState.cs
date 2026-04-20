using UnityEngine;

public abstract class EnemyBehaviorState
{
    protected EnemyController controller;
    protected EnemyData data;
    public EnemyBehaviorState(EnemyData data, EnemyController controller)
    {
        this.data = data;
        this.controller = controller;
    }

    public virtual void Enter() { }
    public virtual void Update() { }
    public virtual void Exit() { }
}
