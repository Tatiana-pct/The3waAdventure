using UnityEngine;

#region States

public enum Mouvement
{
    IDLE,
    WALK,
    SPRINT,
    DASH,
}

#endregion

public class MouvementStateMachine : MonoBehaviour
{
    private Animator _animator;
    private PlayerController _playerController;
    private PlayerInput _PlayerInput;
    private HealthStateMachine _HealthStateMachine;
    private PlayerAbilityTracker _PlayerAbilityTracker;
    
     

    private Mouvement _currentState;

#region Public properties

    public Mouvement CurrentState { get => _currentState; private set => _currentState = value; }

    #endregion

#region Unity Life Cycles

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
        _PlayerInput = GetComponent<PlayerInput>();
        _HealthStateMachine = GetComponent<HealthStateMachine>();
        _PlayerAbilityTracker = GetComponent<PlayerAbilityTracker>();
    }
    private void Start()
    {
        
    }
    private void Update()
    {
        OnStateUpdate(CurrentState);
        //Debug.Log(CurrentState.ToString());
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

#endregion

#region State Machine

    private void OnStateEnter(Mouvement state)
    {
        switch (state)
        {
            case Mouvement.IDLE:
                OnEnterIdle();
                break;
            case Mouvement.WALK:
                OnEnterWalk();
                break;
            case Mouvement.SPRINT:
                OnEnterSprint();
                break;
            case Mouvement.DASH:
                OnEnterDash();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(Mouvement state)
    {
        switch (state)
        {
            case Mouvement.IDLE:
                OnUpdateIdle();
                break;
            case Mouvement.WALK:
                OnUpdateWalk();
                break;
            case Mouvement.SPRINT:
                OnUpdateSprint();
                break;
            case Mouvement.DASH:
                OnUpdateDash();
                break;
        }
    }
    private void OnStateFixedUpdate(Mouvement state)
    {
        switch (state)
        {
            case Mouvement.IDLE:
                OnFixedUpdateIdle();
                break;
            case Mouvement.WALK:
                OnFixedUpdateWalk();
                break;
            case Mouvement.SPRINT:
                OnFixedUpdateSprint();
                break;
            case Mouvement.DASH:
                OnFixedUpdateDash();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(Mouvement state)
    {
        switch (state)
        {
            case Mouvement.IDLE:
                OnExitIdle();
                break;
            case Mouvement.WALK:
                OnExitWalk();
                break;
            case Mouvement.SPRINT:
                OnExitSprint();
                break;
            case Mouvement.DASH:
                OnExitDash();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    public void TransitionToState(Mouvement toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

#endregion

#region State IDLE

    private void OnEnterIdle()
    {
        _playerController.Idle();
        _animator.SetBool("IsIdle", true);
        

    }
    private void OnUpdateIdle()
    {
       if(_PlayerInput.HasMouvement )
        {
            
            if (_PlayerInput.Sprint )
            {
                TransitionToState(Mouvement.SPRINT);
            }
            else
            {
            TransitionToState(Mouvement.WALK);
            }
        }
       
       
       
    }
    private void OnFixedUpdateIdle()
    {
    }
    private void OnExitIdle()
    {
        _animator.SetBool("IsIdle", false);
        
    }

#endregion

#region State WALK

    private void OnEnterWalk()
    {
        _animator.SetBool("IsWalking", true);
    }
    private void OnUpdateWalk()
    {
        if(!_PlayerInput.HasMouvement)
        {
            TransitionToState(Mouvement.IDLE);
        }
        else 
        {
            if ( _PlayerInput.Sprint && _PlayerAbilityTracker.CanSprint)
            {

                TransitionToState(Mouvement.SPRINT);
            }
            if(_PlayerInput.Dash && _PlayerAbilityTracker.CanDash)
            {
                
                TransitionToState(Mouvement.DASH);
            }

        }


    }
    private void OnFixedUpdateWalk()
    {
        _playerController.Walk();
    }
    private void OnExitWalk()
    {
        _animator.SetBool("IsWalking", false);
    }

#endregion

#region State SPRINT

    private void OnEnterSprint()
    {
        _animator.SetBool("IsSprinting", true);
    }
    private void OnUpdateSprint()
    {
        if(_PlayerInput.HasMouvement)
        {
            if (_PlayerInput.Dash && _PlayerAbilityTracker.CanDash)
            {
                TransitionToState(Mouvement.DASH);
            }
            if (!_PlayerInput.Sprint)
            {
            TransitionToState(Mouvement.WALK);

            }
        }
        else
        {
            TransitionToState(Mouvement.IDLE);
            
        }
        
    }
    private void OnFixedUpdateSprint()
    {
        _playerController.Sprint();
    }
    private void OnExitSprint()
    {

        _animator.SetBool("IsSprinting", false);
    }

#endregion

#region State DASH

    private void OnEnterDash()
    {
        _playerController.StartDash();

        _animator.SetBool("IsDashing", true);
        
    }
    private void OnUpdateDash()
    {
        if(_playerController.IsEndDashing)
        {
            if (_PlayerInput.HasMouvement)
            {
                if (_PlayerInput.Sprint && _PlayerAbilityTracker.CanSprint)
                {
                    TransitionToState(Mouvement.SPRINT);
                }
                else
                {
                    TransitionToState(Mouvement.WALK);
                }
            }
            else

                TransitionToState(Mouvement.IDLE);
            
        }

    }
    private void OnFixedUpdateDash()
    {
        _playerController.Dash();   
    }
    private void OnExitDash()
    {
        _animator.SetBool("IsDashing", false);
    }

    #endregion

   //private void OnGUI()
   //{
   //    // On affiche l'état en cours pour le debug
   //    GUIStyle style = new GUIStyle() { fontSize = 50, fontStyle = FontStyle.Bold };
   //    style.normal.textColor = Color.white;
   //    GUI.Label(new Rect(50, 50, 100, 100), _currentState.ToString(), style);
   //}

}
