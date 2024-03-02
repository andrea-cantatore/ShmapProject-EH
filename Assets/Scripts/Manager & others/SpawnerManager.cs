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
    private int _spawnQuantity;
    private int _difficultySetter1, _difficultySetter2;
    private float _timer;
    

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

    private void Start()
    {
        SpawnEnemy();
    }

    private void Update()
    {
        _timer += Time.deltaTime;
        if (_timer > _spawnInterval)
        {
            SpawnEnemy();
            _timer = 0;
        }
    }

    void SpawnEnemy()
    {
        if (_spawnEnabled)
        {
            for (int i = 0; i <= _spawnQuantity; i++)
            {
                int randomIndex = Random.Range(0, _enemyPrefabs.Length);
            
                float yPos = Random.Range(-8f, 8f);
                Vector3 spawnPosition = new Vector3(22f, yPos, 0f);
                Instantiate(_enemyPrefabs[randomIndex], spawnPosition,quaternion.identity);
            }
        }
    }
    private void ScoreUp(int quantity)
    {
        _score += quantity;
        if (_score > _difficultySetter1)
        {
            _spawnInterval -= 0.2f;
            _difficultySetter1 += 400;
        }
        if (_score > _difficultySetter2 )
        {
            _spawnQuantity++;
            _difficultySetter2 += 1000;
        }
        
    }
    private void SpawnDisabler()
    {
        
        _spawnEnabled = false;
        GameObject bossObj = Instantiate(_bossPrefab, new Vector3(16, 0, 0), quaternion.identity);
        
        
    }
}
