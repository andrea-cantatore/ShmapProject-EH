using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScript : MonoBehaviour
{
    [SerializeField] private float _movementSpeed;
    
    void Update()
    {
        transform.Translate(Vector2.right * (_movementSpeed * Time.deltaTime));
        if (gameObject.activeSelf)
        {
            StartCoroutine(WaitToDeactivate());
        } 
    }
    private IEnumerator WaitToDeactivate()
    {
        yield return new WaitForSeconds(1.4f);
        gameObject.SetActive(false);
    }
}
