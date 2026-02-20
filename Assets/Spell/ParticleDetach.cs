using System.Collections;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class ParticleDetach : MonoBehaviour
{
    public void DetachParticles(ParticleSystem particles)
    {
        particles.transform.parent = null;
        
        StartCoroutine(DestroyParticleSystem(particles));
    }

    private static IEnumerator DestroyParticleSystem(ParticleSystem particles)
    {
        // Stop the particles from playing and then destroy them after all of the existing particles die
        particles.Stop(true, ParticleSystemStopBehavior.StopEmitting);
        while (particles.IsAlive())
        {
            yield return null;
        }
        Destroy(particles.gameObject);
    }
}
