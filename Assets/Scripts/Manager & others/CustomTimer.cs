using System;
using UnityEditor.Timeline.Actions;
using UnityEngine;

[System.Serializable]
public class CustomTimer
{
    public Action OnCompleated;
    private float _duration;
    private float _elapsedTime;

    private bool _isRunning;

    public CustomTimer(float duration)
    {
        this._duration = duration;
        this._elapsedTime = 0f;
        this._isRunning = false;
    }
    
    public void Start(Action onCompleated = null)
    {
        OnCompleated = onCompleated;
        _isRunning = true;
    }
    
    public bool TimerUpdate()
    {
        if (!_isRunning)
        {
            return false;
        }

        _elapsedTime += Time.deltaTime;

        if (_elapsedTime >= _duration)
        {
            OnCompleated?.Invoke();
            _elapsedTime = 0f;
            _isRunning = false;
            return false;
        }
        return true;
    }
    
    public void Reset()
    {
        _elapsedTime = 0f;
        _isRunning = false;
    }
}
