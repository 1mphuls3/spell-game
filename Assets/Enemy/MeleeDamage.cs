using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private MeleeData data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.GetComponent<PlayerController>();
            Health playerHealth = player.GetComponent<Health>();

            playerHealth.Damage(data.damage);
        }
    }
}
