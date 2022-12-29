using UnityEngine;

#region States

public enum Health
{
    AWAKE,
    HURT,
    DEAD,
}

#endregion

public class HealthStateMachine : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
    PlayerController _playerController;
    Animator _animator;

    private Health _currentState;
    


#region Public properties

    public Health CurrentState { get => _currentState; private set => _currentState = value; }

#endregion

#region Unity Life Cycles

    private void Start()
    {
       _playerController = GetComponent<PlayerController>();
        _animator = GetComponentInChildren<Animator>();


    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

#endregion

#region State Machine

    private void OnStateEnter(Health state)
    {
        switch (state)
        {
            case Health.AWAKE:
                OnEnterAwake();
                break;
            case Health.HURT:
                OnEnterHurt();
                break;
            case Health.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(Health state)
    {
        switch (state)
        {
            case Health.AWAKE:
                OnUpdateAwake();
                break;
            case Health.HURT:
                OnUpdateHurt();
                break;
            case Health.DEAD:
                OnUpdateDead();
                break;
        }
    }
    private void OnStateFixedUpdate(Health state)
    {
        switch (state)
        {
            case Health.AWAKE:
                OnFixedUpdateAwake();
                break;
            case Health.HURT:
                OnFixedUpdateHurt();
                break;
            case Health.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(Health state)
    {
        switch (state)
        {
            case Health.AWAKE:
                OnExitAwake();
                break;
            case Health.HURT:
                OnExitHurt();
                break;
            case Health.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    public void TransitionToState(Health toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

#endregion

#region State AWAKE

    private void OnEnterAwake()
    {
    }
    private void OnUpdateAwake()
    {
        if(_playerController.IsHurt)
        {
            TransitionToState(Health.HURT);
        }
    }
    private void OnFixedUpdateAwake()
    {
    }
    private void OnExitAwake()
    {
    }

#endregion

#region State HURT

    private void OnEnterHurt()
    {
        _playerController.StartHurt();
        _animator.SetBool("IsHurting", true);
        
        
    }
    private void OnUpdateHurt()
    {
        
        if(_playerController.IsEndHurting)
        {
            if(_playerManager.Currenthealth > 0)
            {
                TransitionToState(Health.AWAKE);
                return;
            }
            else
            {
                TransitionToState(Health.DEAD);
                return;   
            }
        }

        _playerController.DoHurt();
    }
    private void OnFixedUpdateHurt()
    {
    }
    private void OnExitHurt()
    {
        _animator.SetBool("IsHurting", false);
    }

#endregion

#region State DEAD

    private void OnEnterDead()
    {
        _animator.SetTrigger("IsDead");
        _playerController.DoDead();
       
    }
    private void OnUpdateDead()
    {
    }
    private void OnFixedUpdateDead()
    {
    }
    private void OnExitDead()
    {
       
    }

    #endregion

   //private void OnGUI()
   //{
   //    // On affiche l'état en cours pour le debug
   //    GUIStyle style = new GUIStyle() { fontSize = 50, fontStyle = FontStyle.Bold };
   //    style.normal.textColor = Color.white;
   //    GUI.Label(new Rect(50, 200, 100, 100), _currentState.ToString(), style);
   //}

}
