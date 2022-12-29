using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ennemy/Manager", fileName = "EnnemyManager")]
public class EnnemyManager : ScriptableObject
{
    #region HEALTH/LIFE MANAGER
    [Header("HEALTH / LIFE MANAGER")]
    [Space(10)]
    [SerializeField] private int _maxHealth = 5;
    [SerializeField] private int _currenthealth;
    [SerializeField] private int _maxlifeCount = 5;
    [SerializeField] private int _currentLifeCount; 
    [SerializeField] EnnemyManager _ennemyManager;
    #endregion

    #region PROPRIETER
    public int MaxHealth { get => _maxHealth; }
    public int Currenthealth { get => _currenthealth; set => _currenthealth = value; }
    public int MaxlifeCount { get => _maxlifeCount; }
    public int CurrentLifeCount { get => _currentLifeCount; set => _currentLifeCount = value; }

    public bool IsAlive { get { return _currenthealth > 0; } }
    #endregion

    
}
