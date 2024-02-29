using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class PlayerController : MonoBehaviour
{
    //general variables
    
    private Rigidbody2D _rb => GetComponent<Rigidbody2D>();
    [Header("Generals")]
    [SerializeField] private GameObject _hitbox;
    [SerializeField] private int _playerHP;
    private CustomTimer _timer;
    
    
    
    
    [Header("Checks")]
    private bool _isPlayerAlive = true, _isAbleToShoot = true;
    
  
    [Header("Movement")]
    [SerializeField, Tooltip("lower - higher movement speed")]private Vector2 _speedRange;
    private Vector2 _movementDirections;
    private float _movementSpeed; //setted in awake
    [SerializeField] private Vector2 mapLimitX, mapLimitY;
    
    [Header("ShootAndBoombs")]
    [SerializeField] private float _shootDelay;
    private float _fireElapseTime;
    private Transform _shootPoint1, _shootPoint2, _shootPoint3, _shootPoint4, _shootPoint5;
    private int _powerUpLevel = 1;

    private int _bombCounter;
    
    private void Awake()
    {
        _shootPoint1 = GameObject.Find("BulletSpawnPoint1").GetComponent<Transform>();
        _shootPoint2 = GameObject.Find("BulletSpawnPoint2").GetComponent<Transform>();
        _shootPoint3 = GameObject.Find("BulletSpawnPoint3").GetComponent<Transform>();
        _shootPoint4 = GameObject.Find("BulletSpawnPoint4").GetComponent<Transform>();
        _shootPoint5 = GameObject.Find("BulletSpawnPoint5").GetComponent<Transform>();
        _hitbox.SetActive(false);
        _movementSpeed = _speedRange.y;
    }
    private void Start()
    {
        _timer = new CustomTimer(_shootDelay);
        _timer.Start();
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
                _hitbox.SetActive(true);
                _movementSpeed = _speedRange.x;
            }
            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                _hitbox.SetActive(false);
                _movementSpeed = _speedRange.y;
            }
            if (Input.GetKey(KeyCode.K) && !_timer.TimerUpdate())
            {
                Shoot();
                _timer.Reset();
                _timer.Start();
            }
            
        }
    }
    private void Shoot()
    {
        switch (_powerUpLevel)
        {
            case 1:
                GameObject bullet = ObjectPooler.SharedInstance.GetPooledObject();
                bullet.transform.position = _shootPoint1.position;
                bullet.SetActive(true);
                break;
            case 2:
                GameObject bullet1 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet1.transform.position = _shootPoint2.position;
                bullet1.SetActive(true);
                GameObject bullet2 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet2.transform.position = _shootPoint3.position;
                bullet2.SetActive(true);
                break;
            case 3:
                GameObject bullet3 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet3.transform.position = _shootPoint1.position;
                bullet3.SetActive(true);
                GameObject bullet4 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet4.transform.position = _shootPoint2.position;
                bullet4.SetActive(true);
                GameObject bullet5 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet5.transform.position = _shootPoint3.position;
                bullet5.SetActive(true);
                
                break;
            case 4:
                GameObject bullet6 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet6.transform.position = _shootPoint2.position;
                bullet6.SetActive(true);
                GameObject bullet7 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet7.transform.position = _shootPoint3.position;
                bullet7.SetActive(true);
                GameObject bullet8 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet8.transform.position = _shootPoint4.position;
                bullet8.SetActive(true);
                GameObject bullet9 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet9.transform.position = _shootPoint5.position;
                bullet9.SetActive(true);
                break;
            case 5:
                GameObject bullet10 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet10.transform.position = _shootPoint1.position;
                bullet10.SetActive(true);
                GameObject bullet11 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet11.transform.position = _shootPoint2.position;
                bullet11.SetActive(true);
                GameObject bullet12 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet12.transform.position = _shootPoint3.position;
                bullet12.SetActive(true);
                GameObject bullet13 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet13.transform.position = _shootPoint4.position;
                bullet13.SetActive(true);
                GameObject bullet14 = ObjectPooler.SharedInstance.GetPooledObject();
                bullet14.transform.position = _shootPoint5.position;
                bullet14.SetActive(true);
                break;
            default:
                Debug.Log("seiScemo");
                break;
        }

    }
    
    private void FixedUpdate()
    {
        if (IsPlayerAbleToMove())
        {
            Vector2 movement = _movementDirections.normalized * (_movementSpeed * Time.fixedDeltaTime);
            _rb.MovePosition(_rb.position + movement);
        }
    }
    private bool IsPlayerAbleToMove()
    {
        if (
            (mapLimitX.x >= transform.position.x && _movementDirections.x < 0) ||
            (mapLimitX.y <= transform.position.x && _movementDirections.x > 0) ||
            (mapLimitY.x >= transform.position.y && _movementDirections.y < 0) ||
            (mapLimitY.y <= transform.position.y && _movementDirections.y > 0)
        )
        {
            return false;
        }
        return true;
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.layer == 8)
        {
            if(_powerUpLevel < 5)
            {
                _powerUpLevel++;
                other.gameObject.SetActive(false); 
            }
        }
        else
        {
            _playerHP--;
        }
    }


}
