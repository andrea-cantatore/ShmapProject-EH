using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _hpSprites;
    [SerializeField] private GameObject[] _bombSprites;
    [SerializeField] private TMP_Text _scoreText, _timerText;

    private int currentBomb = 0, currentHp = 2;
    private float _score, _timer;

    private void Awake()
    {
        _score = 0;
        _timer = 0;
    }
    private void OnEnable()
    {
        EventManager.OnBomb += BombChanger;
        EventManager.OnPlayerHp += HpChanger;
        EventManager.OnScoreUp += ScoreChanger;
    }
    private void OnDisable()
    {
        EventManager.OnBomb -= BombChanger;
        EventManager.OnPlayerHp -= HpChanger;
        EventManager.OnScoreUp -= ScoreChanger;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _timerText.text = _timer.ToString();
    }

    private void BombChanger(bool isUsed)
    {
        if (isUsed = true)
        {
            _bombSprites[currentBomb].SetActive(false);
        }
        else
        {
            _bombSprites[currentBomb+1].SetActive(true);
        }
    }
    private void HpChanger(bool isDmg)
    {
        if (isDmg = true)
        {
            _hpSprites[currentHp].SetActive(false);
        }
        else
        {
            _hpSprites[currentHp+1].SetActive(true);
        }
    }
    private void ScoreChanger(int quantity)
    {
        _score += quantity;
        _scoreText.text = _score.ToString();
        if (_score >= 1000)
        {
            EventManager.OnObjectScoreReached?.Invoke();
        }
    }
}
