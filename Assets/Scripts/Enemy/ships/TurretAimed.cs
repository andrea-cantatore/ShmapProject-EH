using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class TurretAimed : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _movementSpeed;
    private Vector3 _targetPos;
    [SerializeField] private float _maxX;
    [SerializeField] private float _fireDelay;
    private float _fireElapsedTime;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    private PlayerController playerController;
    [SerializeField] private int _scoreValue;
    [SerializeField] private GameObject[] powerUpPrefab;

    private void OnEnable()
    {
        EventManager.OnObjectScoreReached += DestroyMe;
    }
    private void OnDisable()
    {
        EventManager.OnObjectScoreReached -= DestroyMe;
    }
    private void Awake()
    {
        playerController = FindObjectOfType<PlayerController>();
        _targetPos = TargPosSetter();
    }
    void Update()
    {
        _fireElapsedTime += Time.deltaTime;
        if (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _movementSpeed * Time.deltaTime);
        }
        
        if (transform.position.x < _maxX && transform.position.x > -_maxX)
        {
            if (_fireElapsedTime >= _fireDelay)
            {
                Shoot();
                _fireElapsedTime = 0;
            }
        }
        
        
        Vector2 direction = playerController.transform.position - transform.position;
        transform.up = direction;
        
        if (_hp <= 0)
        {
            DestroyMe();
            EventManager.OnScoreUp?.Invoke(_scoreValue);
        }
        if (transform.position.x < -_maxX - 3)
        {  
            DestroyMe();
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            _hp -= 1;
        }
    }
    private Vector3 TargPosSetter()
    {
        float newX = -_maxX - 4;
        return new Vector3(newX, transform.position.y, 0);

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position,_shootPoint.rotation);
    }

    private void DestroyMe()
    {
        if (Random.Range(0f, 1f) <= 0.2f)
        {
            int randomIndex = Random.Range(0, powerUpPrefab.Length);
            
            Instantiate(powerUpPrefab[randomIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
    
}
