using System.Collections;
using System.Linq;
using Unity.VectorGraphics;
using UnityEngine;
using UnityEngine.SceneManagement;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class Health : MonoBehaviour
{
    [SerializeField] private SpriteRenderer sprite;
    [SerializeField] private AnimationCurve curve;
    [SerializeField] private float flashDuration = 1f;

    public float currentHealth = 100f;
    public float maxHealth = 100f;

    private WaveManager manager;

    void Start()
    {
        manager = FindObjectsByType<WaveManager>().First();
        currentHealth = maxHealth;    
    }

    public void Heal(float amount)
    {
        currentHealth += amount;
        if (currentHealth > maxHealth)
        {
            currentHealth = maxHealth;
        }
    }

    public void Damage(float amount)
    {
        currentHealth -= amount;
        StartCoroutine(FlashCoroutine());

        // If the health is 0 or less, "kill" the enemy
        if(currentHealth <= 0)
        {
            if (!gameObject.CompareTag("Player"))
            {
                manager.OnEnemyDeath(gameObject);
                Destroy(gameObject);
            }
            else
            {
                SceneManager.LoadScene(0);
            }
        }
    }
    
    // Smoothly transition the flash amount from 1 to 0 using the animation curve
    private IEnumerator FlashCoroutine()
    {
        sprite.material.SetColor("_FlashColor", Color.white);
        float elapsedTime = 0f;
        while (elapsedTime < flashDuration)
        {
            elapsedTime += Time.deltaTime;
            float t = elapsedTime / flashDuration;
            sprite.material.SetFloat("_FlashAmount", Mathf.Lerp(1f, 0f, curve.Evaluate(t)));
            yield return null;
        }
    }
}
