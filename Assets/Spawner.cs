using System.Collections;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class Spawner : MonoBehaviour
{
    
    [SerializeField] Transform[] SpawnPoints;
    [SerializeField] GameObject[] Enemys;
    [SerializeField] int enemyCount;
    GameObject enemy;
    Transform spawnPoint;
    
    void Start()
    {
        StartCoroutine(Spawn());
    }

    void Update()
    {
        
    }

    IEnumerator Spawn()
    {
        for (int i = 0; i < enemyCount; i++)
        {
            yield return new WaitForSeconds(0.5f);
            EnemySelector();
            SpawnPointSelector();
            Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);  
        }
    }
    
    void EnemySelector()
    {
        int rnd = Random.Range(0, Enemys.Length);
        enemy = Enemys[rnd];
    }
    void SpawnPointSelector()
    {
        int rnd = Random.Range(0, SpawnPoints.Length);
        spawnPoint = SpawnPoints[rnd];
    }
}
