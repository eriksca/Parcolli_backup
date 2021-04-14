using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab; //prefab del nemico
    [SerializeField] private int maxDistance; //distanza massima dal punto di spawn
    [SerializeField] private int spawnTime; //tempo tra uno spawn e l'altro
    [SerializeField] private int maxEnemies; //numero massimo di nemici 
    private List<GameObject> spawners = new List<GameObject>(); //lista degli spawnpoint
    public static int enemyCount = 0; //numero di nemici presenti

    // Start is called before the first frame update
    void Start()
    {
        spawners.AddRange(GameObject.FindGameObjectsWithTag("EnemySpawner"));
        StartCoroutine(SpawnEnemy());
    }

    // Update is called once per frame
    void Update()
    {

    }
    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (enemyCount < maxEnemies)//sostutuire true a bool che indica che i nemici possono spawnare
            {
                GameObject enemy = Instantiate(enemyPrefab, RandomNavSphere(), new Quaternion(0, 0, 0, 1));
                enemyCount++;
            }
            yield return new WaitForSeconds(spawnTime);
        }

    }
    private Vector3 RandomNavSphere()
    {
        Vector3 randomDirection = Random.insideUnitSphere * maxDistance;
        randomDirection += spawners[Random.Range(0, spawners.Count)].transform.position;
        NavMeshHit hit;
        NavMesh.SamplePosition(randomDirection, out hit, maxDistance, -1);
        return hit.position;
    }
}
