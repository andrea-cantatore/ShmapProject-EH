using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private CustomTimer _timer = new CustomTimer(1.5f);
    private void OnEnable()
    {
        _timer.Start(DisableBullet);
    }
    private void OnDisable()
    {
        _timer.Reset();
    }
    void Update()
    {
        transform.Translate(Vector2.right * (_movementSpeed * Time.deltaTime));
        _timer.TimerUpdate();
    }

    private void DisableBullet()
    {
        gameObject.SetActive(false);
    }

}
