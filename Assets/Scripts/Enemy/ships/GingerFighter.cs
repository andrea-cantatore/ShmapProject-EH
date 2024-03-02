using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class GingerFighter : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _movementSpeed;
    private Vector3 _targetPos;
    [SerializeField] private float _maxX, _maxY;
    [SerializeField] float _fireDelay;
    private float _fireElapsedTime;
    private float m_timer;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    private PlayerController playerController;
    [SerializeField] private int _scoreValue;
    
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
        _targetPos = TargPosSetter(false);
    }
    void Update()
    {
        m_timer += Time.deltaTime;
        _fireElapsedTime += Time.deltaTime;
        if (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _movementSpeed * Time.deltaTime);
        }
        if (m_timer > 10)
        {
            _targetPos = TargPosSetter(true);
        }
        if (transform.position.x < _maxX && transform.position.x > -_maxX)
        {
            if (_fireElapsedTime >= _fireDelay && m_timer < 12)
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
        if (transform.position.x < -_maxX - 2)
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
    private Vector3 TargPosSetter(bool isGoingAway)
    {
        float newX;
        float newY;
        if (!isGoingAway)
        {
            newX = Random.Range(0, _maxX);
            newY = Random.Range(-_maxY, _maxY);
        }
        else
        {
            newX = -25f;
            newY = transform.position.y;
        }
        return new Vector3(newX, newY, 0);

    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(_bulletPrefab, _shootPoint.position,_shootPoint.rotation);
        
    }
    private void DestroyMe()
    {
        Destroy(gameObject);
    }
    
    
}
