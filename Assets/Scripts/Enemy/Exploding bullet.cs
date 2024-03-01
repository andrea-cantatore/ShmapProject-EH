using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explodingbullet : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    private CustomTimer _timer = new CustomTimer(2f);
    [SerializeField] private GameObject _smallBulletPrefab;
    
    private void OnEnable()
    {
        _timer.Start(BulletExplosion);
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
        if ( other.gameObject.layer == 11)
        {
            DisableBullet();
        }
    }

    private void DisableBullet()
    {
        Destroy(gameObject);
    }
    private void BulletExplosion()
    {
        for (int i = 0; i < 4; i++)
        {
            float angle = i * 90f + 45f;
            Quaternion rotation = Quaternion.Euler(0f, 0f, angle);
            GameObject bullet = Instantiate(_smallBulletPrefab, transform.position, rotation);
        }
        Destroy(gameObject);
    }
}
