using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossWall : MonoBehaviour
{
    private Vector3 _targetPos;
    [SerializeField] private float _maxX;
    [SerializeField] private float _movementSpeed;

    private void Awake()
    {
        _targetPos = TargPosSetter();
    }
    void Update()
    {
        
        if (transform.position != _targetPos)
        {
            transform.position = Vector3.MoveTowards(transform.position, _targetPos, _movementSpeed * Time.deltaTime);
        }
        
        if (transform.position.x < -_maxX - 6)
        {  
            Destroy(gameObject);
        }
    }
    
    private Vector3 TargPosSetter()
    {
        float newX = -_maxX - 7;
        return new Vector3(newX, transform.position.y, 0);

    }
}
