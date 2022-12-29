using UnityEngine;

[CreateAssetMenu(menuName = "Player/Manager", fileName = "PlayerManager")]
public class PlayerManager : ScriptableObject
{
    #region HEALTH/LIFE MANAGER

    [Header("HEALTH / LIFE MANAGER")]
    [Space(10)]
    [SerializeField] private int _maxHealth = 100;
    [SerializeField] private int _currenthealth = 100;
    [SerializeField] private int _maxlifeCount = 5;
    [SerializeField] private int _currentLifeCount; 
    #endregion

    #region TIME MANAGER
    [Header("TIME MANAGER")]
    [Space(10)]
    [SerializeField] private float _attackTime;
    public float AttackTime { get => _attackTime;}

    #endregion


    #region PROPRIETER

    public int MaxHealth { get => _maxHealth; set => _maxHealth = value; }
    public int Currenthealth { get => _currenthealth; set => _currenthealth = value; }
    public int MaxlifeCount { get => _maxlifeCount; set => _maxlifeCount = value; }
    public int CurrentLifeCount { get => _currentLifeCount; set => _currentLifeCount = value; }

    #endregion






    // Start is called before the first frame update
    void Start()
    {
        _currenthealth = _maxHealth;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
