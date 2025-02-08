using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] cratePrefabs;
    public Transform enemyParent;
    public float spawnRangeX, spawnRangeZ;

    public int enemyCount, enemyWave = 1;
    public float crateStartDelay, crateSpawnInterval;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        EnemySpawning(enemyWave);
    }

    // Update is called once per frame
    void Update()
    {
        enemyCount = FindObjectsByType<EnemyController>(FindObjectsSortMode.None).Length;
        if (enemyCount == 0)
        {
            enemyWave++;
            EnemySpawning(enemyWave);
        }
    }
    void EnemySpawning(int enemiesToSpawn)
    {
        for (int e = 0; e < enemiesToSpawn + 4; e++)
        {
            int randomIndex = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomIndex], EnemySpawnPosition(), Quaternion.identity, enemyParent);

        }
    }
    private Vector3 EnemySpawnPosition()
    {
        float spawnPosX = Random.Range(-spawnRangeX, spawnRangeX);
        float spawnPosZ = Random.Range(-spawnRangeZ, spawnRangeZ);
        Vector3 randomSpawn = new Vector3(spawnPosX, 0, spawnPosZ);
        return randomSpawn;
    }
}
