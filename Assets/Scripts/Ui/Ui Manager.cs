using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UiManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _hpSprites;
    [SerializeField] private GameObject[] _bombSprites;
    [SerializeField] private TMP_Text _scoreText, _timerText;
    [SerializeField] private GameObject _youWin, _youLose;
    
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
        EventManager.OnBossDeath += YouWinShower;
        EventManager.OnPlayerDeath += YouLoseShower;
    }
    private void OnDisable()
    {
        EventManager.OnBomb -= BombChanger;
        EventManager.OnPlayerHp -= HpChanger;
        EventManager.OnScoreUp -= ScoreChanger;
        EventManager.OnBossDeath -= YouWinShower;
        EventManager.OnPlayerDeath -= YouLoseShower;
    }
    private void Update()
    {
        _timer += Time.deltaTime;
        _timerText.text = _timer.ToString();
    }

    private void BombChanger(bool isUsed)
    {
        if (isUsed)
        {
            _bombSprites[currentBomb].SetActive(false);
            currentBomb--;
        }
        else
        {
            _bombSprites[currentBomb+1].SetActive(true);
            currentBomb++;
        }
    }
    private void HpChanger(bool isDmg)
    {
        if (isDmg)
        {
            _hpSprites[currentHp].SetActive(false);
            currentHp--;
        }
        else
        {
            _hpSprites[currentHp+1].SetActive(true);
            currentHp++;
        }
    }
    private void ScoreChanger(int quantity)
    {
        _score += quantity;
        _scoreText.text = _score.ToString();
        if (_score >= 4000)
        {
            EventManager.OnObjectScoreReached?.Invoke();
        }
    }
    private void YouWinShower()
    {
        _youWin.SetActive(true);
    }
    private void YouLoseShower()
    {
        _youLose.SetActive(true);
    }
    public void BackToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
