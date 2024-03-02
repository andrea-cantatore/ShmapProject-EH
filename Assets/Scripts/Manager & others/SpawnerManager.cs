using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class SpawnerManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _enemyPrefabs;
    [SerializeField] private float _spawnInterval;
    [SerializeField] private bool _spawnEnabled = true;
    [SerializeField] private GameObject _bossPrefab;
    private int _score;

    private void OnEnable()
    {
        EventManager.OnScoreUp += ScoreUp;
        EventManager.OnObjectScoreReached += SpawnDisabler;
    }
    private void OnDisable()
    {
        EventManager.OnScoreUp -= ScoreUp;
        EventManager.OnObjectScoreReached += SpawnDisabler;
    }

    void Start()
    {
        InvokeRepeating("SpawnEnemy", 0f, _spawnInterval);
    }

    void SpawnEnemy()
    {
        if (_spawnEnabled)
        {
            int randomIndex = Random.Range(0, _enemyPrefabs.Length);
            
            float yPos = Random.Range(-8f, 8f);
            Vector3 spawnPosition = new Vector3(22f, yPos, 0f);
            Instantiate(_enemyPrefabs[randomIndex], spawnPosition,quaternion.identity);
        }
    }
    private void ScoreUp(int quantity)
    {
        _score += quantity;
        if (_score % 100 == 0)
        {
            if (_score % 500 == 0)
            {
                _spawnInterval = _spawnInterval % 2;
            }
            else
            {
                _spawnInterval -= 0.5f;
            }
        }
    }
    private void SpawnDisabler()
    {
        _spawnEnabled = false;
        GameObject bossObj = Instantiate(_bossPrefab, new Vector3(22, 0, 0), Quaternion.identity);
        bossObj.transform.position =
            Vector3.MoveTowards(bossObj.transform.position, new Vector3(16, 0, 0), 20 * Time.deltaTime);
    }
}
