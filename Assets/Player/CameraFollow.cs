using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Vector3 offset;
    [SerializeField] private Transform followTarget;
    [SerializeField] private float baseSpeed = 3f;
    [SerializeField] private float speedMultiplier = 0.5f;
    [SerializeField] private float cursorOffsetDistance = 1f;

    private Camera cam;

    void Start()
    {
        cam = GetComponent<Camera>();
    }

    void LateUpdate()
    {
        Vector2 position = followTarget.transform.position;

        // Get the cursors direction relative to the player
        Vector2 cursorWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 cursorDir = (cursorWorldPos - position).normalized * cursorOffsetDistance;

        Vector3 targetPos = new Vector3(position.x + cursorDir.x, position.y + cursorDir.y, offset.z);

        float distance = Vector3.Distance(transform.position, targetPos);
        float dynamicSpeed = baseSpeed + distance * speedMultiplier;

        transform.position = Vector3.MoveTowards(transform.position, targetPos, dynamicSpeed * Time.deltaTime);
    }
}
