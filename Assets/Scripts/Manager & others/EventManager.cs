using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static Action OnPlayerGotDmg;
    public static Action OnBombUse;
    public static Action<int> OnScoreUp;
    public static Action OnObjectScoreReached;
    public static Action OnBossDeath;
    public static Action OnPlayerDeath;
}
