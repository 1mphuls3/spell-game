using Unity.VisualScripting;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{
    public EnemyBehaviorState currentState;

    private void Update()
    {
        currentState?.Update();
        ControllerUpdate();
    }
    public abstract void ControllerUpdate();

    public void SwitchState(EnemyBehaviorState state)
    {
        currentState?.Exit();
        currentState = state;
        currentState.Enter();
    }
}
