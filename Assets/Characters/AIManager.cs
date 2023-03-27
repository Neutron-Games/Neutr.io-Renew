using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [Header("Enemy Parameters")]
    public int maxEnemyCount = 14;
    public int minEnemyCount = 10;
    public GameObject[] enemyPrefab;
    int enemyCount;
    [Header("Golem Parameters")]
    public GameObject golemPrefab;
    int count;
    bool enemySpawning;
    bool golemSpawning;
    [SerializeField] public static Transform playerTransform;
    Transform[] enemySpawnPoints;
    Transform[] golemSpawnPoints;

    private void Start()
    {
        // playerTransform = PlayGroundManager.instance.player.transform;
        StartCoroutine(GolemSpawn());
    }

    private void Update()
    {
        enemyCount = transform.GetChild(0).childCount;
        while (count < 10)
        {
            EnemySpawn(Random.Range(100, 2000));
            count++;
        }

        if (enemyCount < minEnemyCount && !enemySpawning)
        {
            enemySpawning = true;
            EnemySpawn(100);
        }
        else if (enemyCount < maxEnemyCount && enemyCount > minEnemyCount && !enemySpawning)
        {
            enemySpawning = true;
            EnemySpawn(100);
        }
    }


    void EnemySpawn(int scr)
    {
        Transform newEnemy;
        newEnemy = Instantiate(enemyPrefab[Random.Range(0, 3)], EnemyRandomPositionGenerator(), Quaternion.identity).transform;
        newEnemy.SetParent(transform.GetChild(0));
        newEnemy.name = GetNameList.names[Random.Range(0, 204)];
        // newEnemy.GetComponent<Score>().score = scr;
        enemySpawning = false;
    }

    Vector3 EnemyRandomPositionGenerator()
    {
        Vector3 randomPos = Random.insideUnitSphere * 75 + transform.position;
        UnityEngine.AI.NavMeshHit hit;
        UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, 75, UnityEngine.AI.NavMesh.AllAreas);
        return hit.position;
    }


    IEnumerator GolemSpawn()
    {
        Transform newGolem;
        newGolem = Instantiate(golemPrefab, GolemRandomPositionGenerator(), Quaternion.identity).transform;
        newGolem.SetParent(transform.GetChild(1));
        yield return new WaitForSeconds(Random.Range(2.5f, 5f));
        StartCoroutine(GolemSpawn());
    }

    Vector3 GolemRandomPositionGenerator()
    {
        Vector3 pos = new Vector3(Random.Range(playerTransform.position.x - 40, playerTransform.position.x + 40), 1, Random.Range(playerTransform.position.y - 20, playerTransform.position.y + 30));
        return pos;
    }
}
