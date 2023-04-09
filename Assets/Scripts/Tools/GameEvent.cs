using Manager;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvent : MonoBehaviour
{
    public UnityEvent On_Level_Start;
    public UnityEvent On_Level_Compelet;
    public UnityEvent On_Level_Fail;

    private void Awake()
    {
        GameManager.onLevelStart += OnLevelStart;
        GameManager.onLevelCompelet += OnLevelCompelet;
        GameManager.onLevelFail += OnLevelFail;
    }
    private void OnDestroy()
    {
        GameManager.onLevelStart -= OnLevelStart;
        GameManager.onLevelCompelet -= OnLevelCompelet;
        GameManager.onLevelFail -= OnLevelFail;
    }

    private void OnLevelStart() => On_Level_Start?.Invoke();
    private void OnLevelCompelet() => On_Level_Compelet?.Invoke();
    private void OnLevelFail() => On_Level_Fail?.Invoke();
}
