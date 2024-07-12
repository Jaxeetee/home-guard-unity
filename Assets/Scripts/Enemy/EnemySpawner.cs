using System.Collections;
using MyUtils;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField]
    private SpawnArea[] spawnPoints;

    [SerializeField]
    private Enemy _enemy;

    [SerializeField]
    private bool _canSpawn;

    [SerializeField]
    private float _msBetweenSpawn;
    [SerializeField]
    private string _poolKey;
    private float _frequency;

    private void Awake()
    {
        
    }

    private void Start()
    {
        PooledObject.NewObjectPool(_poolKey, _enemy.gameObject, this.gameObject);
    }

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }
    private IEnumerator SpawnEnemy()
    {
        
        while(_canSpawn)
        {
            int randomIndex = Random.Range(0, spawnPoints.Length);
            Vector3 spawnPoint = spawnPoints[randomIndex].originPoint.position * Random.Range(0, spawnPoints[randomIndex].spawnRadius);
            if (Time.time > _frequency)
            {
                _frequency = Time.time + _msBetweenSpawn / 1000;
                GameObject enemy = PooledObject.GetObject(_poolKey);
                enemy.transform.position = spawnPoint;
                enemy.GetComponent<Enemy>().Initialize(_poolKey);
                
            }
            yield return null;
        }

    }


}