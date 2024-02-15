using UnityEngine;

public class CustomTimer
{
    private float duration; // Durata del timer
    private float elapsedTime; // Tempo trascorso

    private bool isRunning; // Indica se il timer è in esecuzione

    public CustomTimer(float duration)
    {
        this.duration = duration;
        this.elapsedTime = 0f;
        this.isRunning = false;
    }
    
    public void Start()
    {
        isRunning = true;
    }
    
    public bool IsTimerRunning()
    {
        if (!isRunning)
        {
            return false;
        }

        elapsedTime += Time.deltaTime;

        if (elapsedTime >= duration)
        {
            elapsedTime = 0f;
            isRunning = false;
            return true;
        }

        return false;
    }
    
    public void Reset()
    {
        elapsedTime = 0f;
        isRunning = false;
    }
}
