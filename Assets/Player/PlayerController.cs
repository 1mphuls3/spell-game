using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 * The following tutorial was referenced for implementing the Unity InputSystem:
 * https://youtu.be/HmXU4dZbaMw?si=uLiDMg-wpqQocgjy
 */
public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rigidBody;
    [SerializeField] private Camera cam;

    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Animator spriteAnimator;

    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 80f;

    [SerializeField] private PlayerSpellManager spellManager;
    [SerializeField] private GameObject spell;
    public SpellDefinition definition;

    [SerializeField] public float spellDamage = 1f;
    [SerializeField] public float spellSpeed = 1f;
    [SerializeField] public float spellSize = 2f;
    [SerializeField] public float spellRange = 2f;
    [SerializeField] public float spellCooldown = 0.2f;
    [SerializeField] public Texture2D color;
    private float spellCooldownCount = 0f;

    private PlayerInputActions inputManager;
    private InputAction move;
    private InputAction fire;
    private InputAction interact;

    [SerializeField] private PauseMenu pauseMenu;
    private InputAction pause;

    private IInteractable currentInteractable;
    private void Awake()
    {
        inputManager = new PlayerInputActions();
        inputManager.Enable();
    }

    private void OnEnable()
    {
        move = inputManager.Player.Move;
        move.Enable();
        fire = inputManager.Player.Fire;
        fire.Enable();
        fire.performed += Fire;
        interact = inputManager.Player.Interact;
        interact.Enable();
        interact.performed += Interact;
        pause = inputManager.Player.Pause;
        pause.Enable();
        pause.performed += Pause;
    }

    private void OnDisable()
    {
        move.Disable();
        fire.Disable();
        interact.Disable();
        pause.Disable();
    }

    void Update()
    {
        Vector2 position = transform.position;
        Vector2 direction = move.ReadValue<Vector2>();
        Vector2 targetVel = direction * moveSpeed;

        // Smooth movement from current velocity to the maximum
        rigidBody.linearVelocity = Vector2.MoveTowards(rigidBody.linearVelocity, targetVel, acceleration * Time.deltaTime);

        if (Mathf.Abs(rigidBody.linearVelocity.x) >= 0.4f || Mathf.Abs(rigidBody.linearVelocity.y) >= 0.4f)
        {
            spriteAnimator.SetBool("isWalking", true);
        }
        else
        {
            spriteAnimator.SetBool("isWalking", false);
        }

        Vector2 cursorWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 cursorDir = (cursorWorldPos - position).normalized;

        // Face the player toward the cursor
        if (cursorDir.x > 0)
        {
            spriteRenderer.flipX = false;
        }
        else
        {
            spriteRenderer.flipX = true;
        }
        spellCooldownCount += Time.deltaTime;
    }

    private void Fire(InputAction.CallbackContext context)
    {

        Vector2 position = transform.position;
        Vector2 cursorWorldPos = cam.ScreenToWorldPoint(Mouse.current.position.ReadValue());
        Vector2 cursorDir = (cursorWorldPos - position).normalized;

        definition = new SpellDefinition(spellDamage, spellSpeed, spellCooldown, 1f, spellSize,
            spellRange, spellManager.modifiers, color);
        foreach (ModifierDefinition modifier in spellManager.modifiers)
        {
            modifier.CalculateStats(definition);
        }

        if (spellCooldownCount < definition.cooldown) return;
        spellCooldownCount = 0f;

        CastContext castContext = new CastContext(gameObject, position, cursorDir * definition.speed);
        definition.spell = spell.GetComponent<SpellInstance>();

        StartCoroutine(Attack(definition, castContext));
    }

    private IEnumerator Attack(SpellDefinition definition, CastContext context)
    {
        spriteAnimator.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.25f);
        definition.Cast(context);
        yield return new WaitForSeconds(0.25f);
        spriteAnimator.SetBool("isAttacking", false);
    }

    private void Pause(InputAction.CallbackContext context)
    {
        pauseMenu.Pause();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IInteractable interactable = collision.GetComponent<IInteractable>();
        if (interactable != null)
        {
            currentInteractable = interactable;
        }
    }

    private void Interact(InputAction.CallbackContext context)
    {
        currentInteractable.Interact();
    }
}
