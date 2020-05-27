using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public EnemyType[] enemyTypes;
    public float randomRange;
    public int count;
    public Transform[] SpawnPoints;

    private void Start()
    {
      
            for (int i = 0; i < count; i++)
            {

            int randomizedEnemy = UnityEngine.Random.Range(0, enemyTypes.Length);
            CreateEnemy(randomizedEnemy);
            
            }

    }

    private void CreateEnemy(int randomizedEnemy)
    {
        foreach (Transform spawnPoint in SpawnPoints)
        {
            Vector3 rndVal = UnityEngine.Random.insideUnitCircle;
            Vector3 pos = spawnPoint.position + new Vector3(rndVal.x, rndVal.y, rndVal.z) * randomRange;
            GameObject spawnedPrefab = Instantiate(enemyPrefab);
            spawnedPrefab.transform.position = pos;
            spawnedPrefab.GetComponent<EnemyAi>().enemyType = enemyTypes[randomizedEnemy];
        }
    }

}
