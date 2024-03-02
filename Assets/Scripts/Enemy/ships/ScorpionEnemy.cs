using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class ScorpionEnemy : MonoBehaviour
{
    [SerializeField] private float _hp;
    [SerializeField] private float _movementSpeed;
    private Vector3 _targetPos;
    [SerializeField] private float _maxX, _maxY;
    [SerializeField] float _fireDelay;
    private float _fireElapsedTime;
    private float m_timer;
    private int _bulletToShoot = 3;
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private GameObject _bulletPrefab;
    [SerializeField] private int _scoreValue;
    
    private void Start()
    {
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
        if (transform.position != _targetPos)
        {
            Vector2 direction = _targetPos - transform.position;
            transform.up = direction;
        }
        else
        {
            transform.up = Vector2.left;
        }
        
        if (_hp <= 0)
        {
            Destroy(gameObject);
            EventManager.OnScoreUp?.Invoke(_scoreValue);
        }
        if (transform.position.x < -_maxX - 2)
        {  
            Destroy(gameObject);
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
        GameObject[] bullet = new GameObject[_bulletToShoot];
        for (int i = 0; i < bullet.Length; i++)
        {
            float angle;
            Quaternion rotation;
            if (i != 0)
            {
                angle = (i % 2 == 0) ? 45f : -45f;
                rotation = Quaternion.Euler(0f, 0f, angle);
            }
            else
            {
                angle = 0;
                rotation = Quaternion.Euler(0f, 0f, angle);
            }
            bullet[i] = Instantiate(_bulletPrefab, _shootPoint.position, _shootPoint.rotation * rotation);
        }
        
    }
    
    
    

}
