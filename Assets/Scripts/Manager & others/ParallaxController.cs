using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour
{
    [SerializeField] private Transform _camera;
    [SerializeField] private float _movementSpeed, slowingForBoss;
    [SerializeField]private bool isBossFight;

    private void Update()
    {
        transform.Translate(Vector2.down * (_movementSpeed * Time.deltaTime));
        if (transform.position.x < _camera.position.x - 200)
            transform.position = new Vector3(transform.position.x + 500, transform.position.y, transform.position.z);
        if (isBossFight && _movementSpeed > 0f)
            _movementSpeed -= slowingForBoss * Time.deltaTime;
    }
}
