using UnityEngine;

public class MeleeDamage : MonoBehaviour
{
    [SerializeField] private MeleeData data;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Health playerHealth = collision.gameObject.GetComponent<Health>();

            playerHealth.Damage(data.damage);
        }
    }
}
