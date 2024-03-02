using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action<bool> OnPlayerHp;
    public static Action<bool> OnBomb;
    public static Action<int> OnScoreUp;
    public static Action OnObjectScoreReached;
    public static Action OnBossDeath;
    public static Action OnPlayerDeath;
}
