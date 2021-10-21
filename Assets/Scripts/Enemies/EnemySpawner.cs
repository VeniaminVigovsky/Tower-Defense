using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private Enemy enemyToSpawn;
    [SerializeField]
    private NodeGrid grid;
    [SerializeField]
    private Tower tower;

    private List<GameObject> enemyPool;
    private int poolSize = 10;

    private Node[] spawnNodes;

    private bool spawnEnemies;
    private bool _stopUpdateTime;
    private bool invalidSpawnPoint;
    

    private float distance;

    public static event Action TimesUp;

    private int id = 0;

    private void Awake()
    {
        enemyToSpawn.gameObject.SetActive(false);

        distance = 5.5f;
        GeneratePool();
        _stopUpdateTime = false;
    }

    private void OnEnable()
    {
        Tower.TowerDestroyed += StopSpawning;
        Tower.TowerDestroyed += DestroyAllActiveEnemies;
        Tower.TowerDestroyed += StopUpdateTime;
        TimesUp += StopSpawning;
        TimesUp += DestroyAllActiveEnemies;
    }

    private void OnDisable()
    {
        Tower.TowerDestroyed -= StopSpawning;
        Tower.TowerDestroyed -= DestroyAllActiveEnemies;
        Tower.TowerDestroyed -= StopUpdateTime;
        TimesUp -= StopSpawning;
        TimesUp -= DestroyAllActiveEnemies;
    }


    private void Start()
    {
        DefineSpawnPoints();

        spawnEnemies = true;


        StartCoroutine(WaveEnemies());

    }

    private void Update()
    {
        if (GameManager.ElapsedTime > GameManager.RoundTime && !_stopUpdateTime)
        {

            TimesUp?.Invoke();

            
            _stopUpdateTime = true;
        }
    }

    private void GeneratePool()
    {
        enemyPool = new List<GameObject>();

        for (int i = 0; i < poolSize; i++)
        {
            GameObject e = Instantiate(enemyToSpawn.gameObject);
            Enemy enemy = e.GetComponent<Enemy>();
            if (enemy == null) continue;
            enemy.EnemyConstructor(grid, tower, id);
            e.SetActive(false);
            enemyPool.Add(e);
            id++;
        }
    }

    private void StopSpawning()
    {
        spawnEnemies = false;
        
    }

    private void DefineSpawnPoints()
    {
        int spawnPoinsCount = UnityEngine.Random.Range(5, 8);
        spawnNodes = new Node[spawnPoinsCount];
        for (int i = 0; i < spawnPoinsCount; i++)
        {
            invalidSpawnPoint = true;
            do
            {
                int x = UnityEngine.Random.Range(0, grid.GridSizeX);
                int y = UnityEngine.Random.Range(0, grid.GridSizeY);

                spawnNodes[i] = grid.GetNodeByIndeces(x, y);

                invalidSpawnPoint = !spawnNodes[i].IsWalkable || Vector3.Distance(spawnNodes[i].WorldPosition, tower.transform.position) < distance;

            } while (invalidSpawnPoint);
           

        }
    }



    private void DestroyAllActiveEnemies()
    {
        foreach (var enemy in enemyPool)
        {
            if (enemy.activeInHierarchy)
            {
                enemy.GetComponent<Enemy>().DestroySelf();
            }
        }

        Invoke("DestroyAllActiveEnemies", 1f);
    }

    private void SpawnEnemy(Vector3 position)
    {

        GameObject e = GetEnemy();
        if (e == null) return;
        
        e.transform.position = position;
        e.SetActive(true);
    }

    private GameObject GetEnemy()
    {
        if (tower == null || grid == null) return null;


        foreach (var g in enemyPool)
        {
            if (!g.activeInHierarchy)
            {
                return g;
            }
        }


        GameObject e = Instantiate(enemyToSpawn.gameObject);
        Enemy enemy = e.GetComponent<Enemy>();
        if (enemy == null) return null;
        enemy.EnemyConstructor(grid, tower, id++);

        return e;
    }

    private IEnumerator WaveEnemies()
    {
        while (spawnEnemies)
        {
            if (spawnNodes == null) yield break;



            foreach (var spawnNode in spawnNodes)
            {
                SpawnEnemy(spawnNode.WorldPosition);
                yield return new WaitForSeconds(0.2f);
            }

            

            yield return new WaitForSeconds(4.2f);
            DefineSpawnPoints();
        }

        yield break;

    }

    private void StopUpdateTime()
    {
        _stopUpdateTime = true;
    }

}
