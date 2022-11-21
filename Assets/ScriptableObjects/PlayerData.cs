using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PlayerData : ScriptableObject, IResetOnExitAndStart
{
    // Constant variables, set on exiting the editor
    [SerializeField] private Vector2 _initialValue;
    [SerializeField] private bool _movedScene;
    [SerializeField] private float _currentHealth;
    [SerializeField] private float _maxHealth;
    [SerializeField] private float _moveSpeed;
    [SerializeField] private float _biteCoolDown;
    [SerializeField] private float _mineCoolDown;
    [SerializeField] private float _speedCoolDown;

    // Non constant variables, change throughout playtime
    [System.NonSerialized] public Vector2 initialValue;
    [System.NonSerialized] public bool movedScene;
    [System.NonSerialized] public float currentHealth;
    [System.NonSerialized] public float moveSpeed;
    [System.NonSerialized] public float maxHealth;
    [System.NonSerialized] public float biteCoolDown;
    [System.NonSerialized] public float mineCoolDown;
    [System.NonSerialized] public float speedCoolDown;

    public void ResetOnExitAndStart()
    {
        // reset values to default whenever exiting playmode
        initialValue = _initialValue;
        movedScene = _movedScene;
        currentHealth = _currentHealth;
        moveSpeed = _moveSpeed;
        maxHealth = _maxHealth;
        biteCoolDown = _biteCoolDown;
        mineCoolDown = _mineCoolDown;
        speedCoolDown = _speedCoolDown;
    }    
}
