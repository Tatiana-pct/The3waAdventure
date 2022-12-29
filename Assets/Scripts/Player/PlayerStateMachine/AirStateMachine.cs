using UnityEngine;

#region States

public enum AirState
{
    GROUNDED,
    JUMPING,
}

#endregion

public class AirStateMachine : MonoBehaviour
{

    private Animator _animator;
    private PlayerController _playerController;
    private PlayerInput _PlayerInput;
    private MouvementStateMachine _mouvementStateMachine;
    private PlayerAbilityTracker _PlayerAbilityTracker;


    private AirState _currentState;

#region Public properties

    public AirState CurrentState { get => _currentState; private set => _currentState = value; }

#endregion

#region Unity Life Cycles

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _playerController = GetComponent<PlayerController>();
        _PlayerInput = GetComponent<PlayerInput>();
        _mouvementStateMachine = GetComponentInChildren<MouvementStateMachine>();
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

    private void OnStateEnter(AirState state)
    {
        switch (state)
        {
            case AirState.JUMPING:
                OnEnterJumping();
                break;
            case AirState.GROUNDED:
                OnEnterGrounded();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(AirState state)
    {
        switch (state)
        {
            case AirState.JUMPING:
                OnUpdateJumping();
                break;
            case AirState.GROUNDED:
                OnUpdateGrounded();
                break;
        }
    }
    private void OnStateFixedUpdate(AirState state)
    {
        switch (state)
        {
            case AirState.JUMPING:
                OnFixedUpdateJumping();
                break;
            case AirState.GROUNDED:
                OnFixedUpdateGrounded();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(AirState state)
    {
        switch (state)
        {
            case AirState.JUMPING:
                OnExitJumping();
                break;
            case AirState.GROUNDED:
                OnExitGrounded();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    public void TransitionToState(AirState toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

#endregion

#region State JUMPING

    private void OnEnterJumping()
    {
        
        _playerController.StartJump();
        _playerController.Jump();
        _animator.SetBool("IsJumping", true);
        
    }
    private void OnUpdateJumping()
    {
        if(_playerController.IsEndJumping && _playerController.IsOnGround )
        {
            TransitionToState(AirState.GROUNDED);
        }
        else if ( _PlayerInput.Jump  &&_playerController.CanJump && _PlayerAbilityTracker.CanJump )
        {

            TransitionToState(AirState.JUMPING);
        }

    }
    private void OnFixedUpdateJumping()
    {
    }
    private void OnExitJumping()
    {
        _animator.SetBool("IsJumping", false);
    }

#endregion


#region State GROUNDED

    private void OnEnterGrounded()
    {
        _playerController.Gound();
        _animator.SetBool("IsGrounded", true);
    }
    private void OnUpdateGrounded()
    {
        if(_PlayerInput.Jump && _playerController.CanJump && _playerController.IsOnGround &&_PlayerAbilityTracker.CanJump  )
        {
            
            TransitionToState(AirState.JUMPING);
        }
    }
    private void OnFixedUpdateGrounded()
    {
        
    }
    private void OnExitGrounded()
    {

        _animator.SetBool("IsGrounded", false);
    }

    #endregion

   //private void OnGUI()
   //{
   //    // On affiche l'état en cours pour le debug
   //    GUIStyle style = new GUIStyle() { fontSize = 50, fontStyle = FontStyle.Bold };
   //    style.normal.textColor = Color.white;
   //    GUI.Label(new Rect(50, 100, 100, 100), _currentState.ToString(), style);
   //}

}
