using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;

// https://docs.unity3d.com/Packages/com.unity.inputsystem@1.4/manual/Actions.html#creating-actions

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private PlayerInputManager input;
    [SerializeField] private Camera cam;

    [SerializeField] private GameObject bullet;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 80f;

    void Start()
    {
        input.action.Enable();
    }

    void Update()
    {
        Vector2 position = transform.position;
        Vector2 direction = input.action.ReadValue<Vector2>();
        Vector2 targetVel = direction * moveSpeed;

        // Smooth movement from current velocity to the maximum
        rigidBody.linearVelocity = Vector2.MoveTowards(rigidBody.linearVelocity, targetVel, acceleration * Time.deltaTime);

        Vector2 cursorWorldPos = cam.ScreenToWorldPoint(Input.mousePosition);
        Vector2 cursorDir = (position - cursorWorldPos).normalized;

        CastContext context = new CastContext(gameObject, position, cursorDir);
    }
}
