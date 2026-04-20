using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * All code is original work, with Unity Documentation referenced for identifying Unity
 * specific methods and their correct usage and outputs.
 */
public class WaveManager : MonoBehaviour
{
    public List<EnemyType> enemyTypes;
    public Vector2[] spawnRegion;
    public int currentWave = 1;

    public GameObject chestPrefab;
    public Transform chestSpawn;
    private GameObject activeChest;

    public List<GameObject> aliveEnemies = new List<GameObject>();
    private WaveState state;

    void Start()
    {
        StartCoroutine(WaveLoop());
    }

    IEnumerator WaveLoop()
    {
        while (true)
        {
            state = WaveState.ChestPhase;
            yield return StartCoroutine(SpawnChestAndWait());

            StartNextWave();

            state = WaveState.WaitingForEnemies;
            yield return new WaitUntil(() => aliveEnemies.Count == 0);

            currentWave++;
        }
    }

    IEnumerator SpawnChestAndWait()
    {
        activeChest = Instantiate(chestPrefab, chestSpawn.position, Quaternion.identity);

        ChestOpen chest = activeChest.GetComponent<ChestOpen>();

        float timer = 0f;
        float maxWaitAfterOpen = 5f;

        bool taken = false;

        chest.item.OnItemTaken += () =>
        {
            taken = true;
        };

        // Wait until chest is opened
        yield return new WaitUntil(() => taken);

        // Wait a few seconds after pickup/opening
        while (timer < maxWaitAfterOpen)
        {
            timer += Time.deltaTime;
            yield return null;
        }

        Destroy(activeChest);
    }

    public void StartNextWave()
    {
        int budget = GetWaveBudget(currentWave);

        List<GameObject> enemiesToSpawn = new List<GameObject>();

        while (budget > 0)
        {
            EnemyType enemy = ChooseRandomEnemy(budget);
            if (enemy == null) break;

            enemiesToSpawn.Add(enemy.prefab);
            budget -= enemy.cost;
        }

        StartCoroutine(SpawnWave(enemiesToSpawn));

        currentWave++;
    }
    IEnumerator SpawnWave(List<GameObject> enemies)
    {
        foreach (var enemy in enemies)
        {
            Vector3 spawnPoint = new Vector2(Random.Range(spawnRegion[0].x, spawnRegion[1].x), Random.Range(spawnRegion[0].y, spawnRegion[1].y));
            GameObject e = Instantiate(enemy);
            e.transform.position = spawnPoint;
            aliveEnemies.Add(e);

            yield return new WaitForSeconds(0.5f);
        }
    }

    EnemyType ChooseRandomEnemy(int remainingBudget)
    {
        List<EnemyType> valid = enemyTypes.FindAll(e => e.cost <= remainingBudget);
        if (valid.Count == 0) return null;

        return valid[Random.Range(0, valid.Count)];
    }

    int GetWaveBudget(int waveNumber)
    {
        return Mathf.RoundToInt(5 + waveNumber * 2.5f);
    }

    bool IsWaveCleared()
    {
        return aliveEnemies.Count == 0;
    }

    public void OnEnemyDeath(GameObject enemy)
    {
        aliveEnemies.Remove(enemy);
    }
}

[System.Serializable]
public class EnemyType
{
    public GameObject prefab;
    public int cost;
}

public enum WaveState
{
    WaitingForEnemies,
    ChestPhase
}