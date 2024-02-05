using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //general variables
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    private GameObject _bulletSpawner => GameObject.Find("PlBulletSpawnPoint");
    [SerializeField] private GameObject _Hitbox;
    private int _playerHP;
    
    //checks
    private bool _isPlayerAlive = true, _isAbleToShoot = true;
    
    //movement variables
    [SerializeField, Tooltip("lower - higher movement speed")]private Vector2 _speedRange;
    private Vector2 _movementDirections;
    private float _movementSpeed; //setted in awake
    
    //shoot && bombs variables
    [SerializeField] private float _attackDelay;
    private float _fireElapseTime;
    private int _bombCounter;
    private void Awake()
    {
        _Hitbox.SetActive(false);
        _movementSpeed = _speedRange.y;
        _playerHP = 3;
    }
    private void Update()
    {
        if (_playerHP <= 0)
        {
            _isPlayerAlive = false;
        }
        if (_isPlayerAlive)
        {
            _movementDirections.x = Input.GetAxisRaw("Horizontal");
            _movementDirections.y = Input.GetAxisRaw("Vertical");
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _Hitbox.SetActive(true);
                _movementSpeed = _speedRange.x;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _Hitbox.SetActive(false);
                _movementSpeed = _speedRange.y;
            }
        }
    }
    private void FixedUpdate()
    {
        Vector2 movement = _movementDirections.normalized * _movementSpeed * Time.fixedDeltaTime;
        _rb.MovePosition(_rb.position + movement);
    }
}
