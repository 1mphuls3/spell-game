using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class AOEDamage : MonoBehaviour
{
    [SerializeField] private ParticleSystem particles;
    [SerializeField] private ParticleDetach detach;

    // Number of damage to deal per tick
    [SerializeField] private float damage = 1f;
    // Amount of seconds to wait before dealing damage again
    [SerializeField] private float seconds = 1f;
    // Number of seconds the AOE field can exist
    [SerializeField] private float lifeTime = 4f;

    [SerializeField] private float tickCounter = 0;
    [SerializeField] private float age = 0;

    private void Start()
    {
        tickCounter = age = 0;
    }

    private void Update()
    {
        // If the current age is greater than the lifetime, destroy it
        if(age >= lifeTime)
        {
            detach.DetachParticles(particles);
            Destroy(gameObject);
        }
        age += Time.deltaTime;
    }
    private void OnTriggerStay2D(Collider2D other)
    {
        if(other.gameObject.CompareTag("Enemy") && other.gameObject.GetComponent<Health>() != null)
        {
            Health health = other.gameObject.GetComponent<Health>();

            if (tickCounter >= seconds)
            {
                health.Damage(damage);
                tickCounter = 0;
            }
            tickCounter += Time.deltaTime;
        }
    }
}
