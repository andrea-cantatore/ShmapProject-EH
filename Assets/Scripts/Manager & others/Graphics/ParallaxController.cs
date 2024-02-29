using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _movementSpeed, slowingForBoss;
    [SerializeField] private bool isBossFight;
    private float _realTimeMovementSpeed;
    private void Start()
    {
        _realTimeMovementSpeed = _movementSpeed;
    }
    private void Update()
    {
        transform.Translate(Vector2.left * (_realTimeMovementSpeed * Time.deltaTime));
        if (transform.position.x < _camera.position.x - 80)
            transform.position = new Vector3(transform.position.x + 230, transform.position.y, transform.position.z);
        if (isBossFight && _realTimeMovementSpeed > _movementSpeed - 1.5f)
            _realTimeMovementSpeed -= slowingForBoss * Time.deltaTime;
    }
}
