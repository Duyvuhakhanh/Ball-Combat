using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public GameObject[] enemyPrefabs;
    public GameObject[] powerupPrefabs;
    public GameObject bossPrefab;
    public int bossRound;
    public float spawnRange = 9;
    public int enemyCount;
    public int waveNumber = 1;
    void Start()
    {
        int randomPowerup = Random.Range(0, powerupPrefabs.Length);
        Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        SpawnEnemyWave(waveNumber);

    }

    // Update is called once per frame
    void Update()
    {
        //check if this tound is boss round
        enemyCount = FindObjectsOfType<Enemy>().Length;
        if (enemyCount == 0)
        {
            waveNumber++;
            if (waveNumber % bossRound == 0)
            {
                SpawnBossWave(waveNumber);
            }
            else
            {
                SpawnEnemyWave(waveNumber);
            }
        // Spawn Powerup
            int randomPowerup = Random.Range(0, powerupPrefabs.Length);
            Instantiate(powerupPrefabs[randomPowerup], GenerateSpawnPosition(), powerupPrefabs[randomPowerup].transform.rotation);
        }
    }
    private Vector3 GenerateSpawnPosition()
    {
        float spawnPosx = Random.RandomRange(-spawnRange, spawnRange);
        float spawnPosz = Random.RandomRange(-spawnRange, spawnRange);
        Vector3 spawnPos = new Vector3(spawnPosx, 0, spawnPosz);
        return spawnPos;
    }
    void SpawnEnemyWave(int enemiesToSpawn)
    {
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            int randomEnemy = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomEnemy], GenerateSpawnPosition(), enemyPrefabs[randomEnemy].transform.rotation);
        }
    }
    void SpawnBossWave(int currentRound)
    {
        int miniEnemysToSpawn;
        //We dont want to divide by 0!
        if (bossRound != 0)
        {
            miniEnemysToSpawn = currentRound / bossRound;
        }
        else
        {
            miniEnemysToSpawn = 1;
        }
        var boss = Instantiate(bossPrefab, GenerateSpawnPosition(), bossPrefab.transform.rotation);
        boss.GetComponent<Enemy>().miniEnemySpawnCount = miniEnemysToSpawn;
    }
    public void SpawnMiniEnemy(int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            int randomMini = Random.Range(0, enemyPrefabs.Length);
            Instantiate(enemyPrefabs[randomMini], GenerateSpawnPosition(),  
            enemyPrefabs[randomMini].transform.rotation);
        }
    }

}
