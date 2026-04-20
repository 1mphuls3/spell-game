using System.Linq;
using UnityEngine;

public class EnemyData : MonoBehaviour
{
    public Transform player;
    public Rigidbody2D rigidBody;
    public Animator spriteAnimator;
    public SpriteRenderer spriteRenderer;
    public Health health;

    [Header("Movement")]
    public float moveSpeed;
    public float acceleration;

    [Header("Combat")]
    public float attackRange;
    public float detectRange;

    [HideInInspector] public float distanceToPlayer;
    [HideInInspector] public Vector2 directionToPlayer;

    private void Start()
    {
        player = FindObjectsByType<PlayerController>().First().transform;
    }

    void Update()
    {
        Vector2 toPlayer = player.position - transform.position;
        distanceToPlayer = toPlayer.magnitude;
        directionToPlayer = toPlayer.normalized;
    }

}
