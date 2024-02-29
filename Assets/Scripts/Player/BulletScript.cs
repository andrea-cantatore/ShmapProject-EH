using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private CustomTimer _timer = new CustomTimer(2.5f);
    [SerializeField] private bool isAlly;
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
        transform.Translate(Vector2.up * (_movementSpeed * Time.deltaTime));
        _timer.TimerUpdate();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        if ((!isAlly && other.gameObject.layer == 11) || (isAlly && other.gameObject.layer == 20))
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        if (isAlly)
        {
            gameObject.SetActive(false);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    

}
