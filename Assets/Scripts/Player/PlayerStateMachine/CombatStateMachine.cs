using UnityEngine;

#region States

public enum Combat
{
    PEACFULL,
    ATTACKING,
}

#endregion

public class CombatStateMachine : MonoBehaviour
{
    private PlayerController _playerController;
    private PlayerInput _PlayerInput;
    private HealthStateMachine _HealthStateMachine;
    private AirStateMachine _AirStateMachine;
    private Animator _Animator;
    private PlayerAbilityTracker _PlayerAbilityTracker;

    private Combat _currentState;

#region Public properties

    public Combat CurrentState { get => _currentState; private set => _currentState = value; }

#endregion

#region Unity Life Cycles

    private void Start()
    {
        _playerController = GetComponent<PlayerController>();
        _PlayerInput = GetComponent<PlayerInput>();
        _HealthStateMachine = GetComponent<HealthStateMachine>();
        _AirStateMachine = GetComponent<AirStateMachine>();
        _Animator = GetComponentInChildren<Animator>();
        _PlayerAbilityTracker = GetComponent<PlayerAbilityTracker>();
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

    private void OnStateEnter(Combat state)
    {
        switch (state)
        {
            case Combat.PEACFULL:
                OnEnterPeacfull();
                break;
            case Combat.ATTACKING:
                OnEnterAttacking();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(Combat state)
    {
        switch (state)
        {
            case Combat.PEACFULL:
                OnUpdatePeacfull();
                break;
            case Combat.ATTACKING:
                OnUpdateAttacking();
                break;
        }
    }
    private void OnStateFixedUpdate(Combat state)
    {
        switch (state)
        {
            case Combat.PEACFULL:
                OnFixedUpdatePeacfull();
                break;
            case Combat.ATTACKING:
                OnFixedUpdateAttacking();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(Combat state)
    {
        switch (state)
        {
            case Combat.PEACFULL:
                OnExitPeacfull();
                break;
            case Combat.ATTACKING:
                OnExitAttacking();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    public void TransitionToState(Combat toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

#endregion

#region State PEACFULL

    private void OnEnterPeacfull()
    {
       // _Animator.SetBool("IsPeacefull",true);
        
    }
    private void OnUpdatePeacfull()
    {
        if(_PlayerInput.Attack && _PlayerAbilityTracker.CanAttack)
        {
            TransitionToState(Combat.ATTACKING);
        }
    }
    private void OnFixedUpdatePeacfull()
    {
    }
    private void OnExitPeacfull()
    {
        //_Animator.SetBool("IsPeacefull",false);
    }

#endregion

#region State ATTACKING

    private void OnEnterAttacking()
    {
        //_Animator.SetTrigger("IsAttacking");
        _Animator.SetBool("IsAttack", true);
        _playerController.StartAttack();
        

        
    }
    private void OnUpdateAttacking()
    {
       if(_playerController.IsEndAttack)
       {
           TransitionToState(Combat.PEACFULL);
       }
    }
    private void OnFixedUpdateAttacking()
    {
    }
    private void OnExitAttacking()
    {
        _Animator.SetBool("IsAttack", false);
    }

    #endregion


   //private void OnGUI()
   //{
   //    // On affiche l'état en cours pour le debug
   //    GUIStyle style = new GUIStyle() { fontSize = 50, fontStyle = FontStyle.Bold };
   //    style.normal.textColor = Color.white;
   //    GUI.Label(new Rect(50, 150, 100, 100), _currentState.ToString(), style);
   //}

}
