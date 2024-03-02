using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class PurpleSaw : MonoBehaviour
{
    
    [SerializeField] private float _hp;
    [SerializeField] private float _movementSpeed;
    private Vector3 _targetPos;
    [SerializeField] private float _maxX;
    [SerializeField] private float _moveDelay, _timeOnScreen;
    private float _moveElapsedTime,_moveElapsedTimeNotResetting;
    private float m_timer;
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
    void Start()
    {
        playerController = FindObjectOfType<PlayerController>();
        _targetPos = TargPosSetter(false);
    }

    void Update()
    {
        _moveElapsedTime += Time.deltaTime;
        _moveElapsedTimeNotResetting += Time.deltaTime;
        if (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _movementSpeed * Time.deltaTime);
        }
        
        
        if (_moveElapsedTimeNotResetting > _timeOnScreen)
        {
            _targetPos = TargPosSetter(true);
        }
        else if (_moveElapsedTime >= _moveDelay && _moveElapsedTime < _timeOnScreen)
        {
            _targetPos = TargPosSetter(false);
            _moveElapsedTime = 0;
        }
        if (_hp <= 0)
        {
            DestroyMe();
            EventManager.OnScoreUp?.Invoke(_scoreValue);
        }
        if (transform.position.x < -_maxX - 3)
        {
            DestroyMe();
        }
        Rotate();
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            if (transform.position.x < _maxX)
            {
                _hp -= 1;
            }
        }
    }

    

    private void Rotate()
    {
        transform.Rotate(Vector3.forward * (Time.deltaTime * 500));
    }
    private Vector3 TargPosSetter(bool isGoingAway)
    {
        if (isGoingAway)
        {
            return new Vector3(-_maxX - 4, transform.position.y);
        }
        else
        {
            return playerController.transform.position;
        }
    }
    private void DestroyMe()
    {
        if (Random.Range(0f, 1f) <= 0.5f)
        {
            int randomIndex = Random.Range(0, powerUpPrefab.Length);
            
            Instantiate(powerUpPrefab[randomIndex], transform.position, Quaternion.identity);
        }
        Destroy(gameObject);
    }
}
