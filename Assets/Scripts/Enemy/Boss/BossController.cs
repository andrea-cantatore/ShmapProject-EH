using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

public class BossController : MonoBehaviour
{
    [SerializeField] private int _maxHP;
    private int _currentHP;
    private int _phase = 1;

    private PlayerController _playerController;
    [SerializeField] private GameObject _bulletPrefab, _wallPrefab;
    [SerializeField] private float _fireDelay, _wallDelay;
    [SerializeField] private Transform _shotPoint;
    private float _timer, _timer2;
    [SerializeField] private float _maxY, _maxX;

    void Awake()
    {
        _playerController = FindObjectOfType<PlayerController>();
    }

    void Start()
    {
        _currentHP = _maxHP;
        
        Vector3.MoveTowards(transform.position, new Vector3(_maxX - 2,transform.position.y,transform.position.z), 20 * Time.deltaTime);
        transform.up = Vector2.left;
    }

    void Update()
    {
        if (_currentHP <= 0)
        {
            StartNextPhase();
        }
        else
        {
            _timer += Time.deltaTime;
            _timer2 += Time.deltaTime;

            if (_phase == 1 && _timer >= _fireDelay)
            {
                ShootPhase1();
                _timer = 0f;
            }
            if (_phase == 2 && _timer >= _wallDelay)
            {
                GenerateWallsPath();
                _timer = 0f;
            }
            if (_phase == 3 && _timer >= _fireDelay)
            {
                ShootPhase1();
                _timer = 0f;
            }
            if (_phase == 3 && _timer2 >= _wallDelay)
            {
                GenerateWallsPath();
                _timer2 = 0f;
            }
            
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 12)
        {
            _currentHP -= 1;
        }
    }

    void StartNextPhase()
    {
        _phase++;
        if (_phase == 3)
        {
            _fireDelay += _fireDelay;
        }
        if (_phase > 3)
        {
            Destroy(gameObject);
            EventManager.OnBossDeath?.Invoke();
        }
        else
        {
            _currentHP = _maxHP*2;
        }
    }

    void ShootPhase1()
    {
        Vector3 directionToPlayer = (_playerController.transform.position - _shotPoint.position).normalized;
        Quaternion rotationToPlayer = Quaternion.LookRotation(Vector3.forward, directionToPlayer);
        Instantiate(_bulletPrefab, _shotPoint.position, rotationToPlayer);

        Quaternion leftAngle = Quaternion.Euler(0, 0, 90 - 20);
        Instantiate(_bulletPrefab, _shotPoint.position, leftAngle);

        Quaternion rightAngle = Quaternion.Euler(0, 0,90 + 20);
        Instantiate(_bulletPrefab, _shotPoint.position, rightAngle);
    }
    

    void GenerateWallsPath()
    {
        float startX = 22f;
        float endX = startX;
        float minY = -_maxY;
        float maxY = _maxY;
        float length = 7f;
        int wallCount = 3;
        float step = (maxY - minY) / (wallCount - 1);
        
        System.Random random = new System.Random();

        for (int i = 0; i < wallCount; i++)
        {
            float yPos = minY + i * step;
        
            float randomOffset = Random.Range(-0.5f, 0.5f) * step;
            yPos += randomOffset;

            yPos = Mathf.Clamp(yPos, minY, maxY);

            Vector3 currentPosition = new Vector3(startX, yPos, 0f);

            bool isVerticalWall = random.Next(2) == 0;

            if (isVerticalWall)
            {
                Quaternion rotation = Quaternion.Euler(0f, 0f, 90f);
                Instantiate(_wallPrefab, currentPosition, rotation);
            }
            else
            {
                Instantiate(_wallPrefab, currentPosition, Quaternion.identity);
            }
            

            
        }
    }
    

    
}