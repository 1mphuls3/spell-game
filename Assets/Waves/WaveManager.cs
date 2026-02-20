using System.Collections;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class WaveManager : MonoBehaviour
{
    public int currentWave = 0;
    void Start()
    {
        //StartCoroutine(StartWave());
    }

    IEnumerator StartWave()
    {
        yield return null;
    }
}
