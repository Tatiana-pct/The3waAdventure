using UnityEngine;

#region States

public enum EnnemyState
{
    TARGET,
    ATTACK,
    HURT,
    DEAD,
}

#endregion

public class EnnemyStateMachine : MonoBehaviour
{
    private EnnemyController _ennemyController;
    private Animator _animator;
    [SerializeField] EnnemyManager _ennemyManager;    

    private EnnemyState _currentState;

    #region Public properties

    public EnnemyState CurrentState { get => _currentState; private set => _currentState = value; }

    #endregion

    #region Unity Life Cycles
    private void Awake()
    {
         _ennemyController = GetComponent<EnnemyController>();
        _animator = GetComponentInChildren<Animator>();
       
    }

    private void Start()
    {
      
    }
    private void Update()
    {
        Debug.Log(CurrentState);
        
        OnStateUpdate(CurrentState);
       
    }
    private void FixedUpdate()
    {
        OnStateFixedUpdate(CurrentState);
    }

    #endregion

    #region State Machine

    private void OnStateEnter(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.TARGET:
                OnEnterTarget();
                break;
            case EnnemyState.ATTACK:
                OnEnterAttack();
                break;
            case EnnemyState.HURT:
                OnEnterHurt();
                break;
            case EnnemyState.DEAD:
                OnEnterDead();
                break;
            default:
                Debug.LogError("OnStateEnter: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateUpdate(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.TARGET:
                OnUpdateTarget();
                break;
            case EnnemyState.ATTACK:
                OnUpdateAttack();
                break;
            case EnnemyState.HURT:
                OnUpdateHurt();
                break;
            case EnnemyState.DEAD:
                OnUpdateDead();
                break;
        }
    }
    private void OnStateFixedUpdate(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.TARGET:
                OnFixedUpdateTarget();
                break;
            case EnnemyState.ATTACK:
                OnFixedUpdateAttack();
                break;
            case EnnemyState.HURT:
                OnFixedUpdateHurt();
                break;
            case EnnemyState.DEAD:
                OnFixedUpdateDead();
                break;
            default:
                Debug.LogError("OnStateFixedUpdate: Invalid state " + state.ToString());
                break;
        }
    }
    private void OnStateExit(EnnemyState state)
    {
        switch (state)
        {
            case EnnemyState.TARGET:
                OnExitTarget();
                break;
            case EnnemyState.ATTACK:
                OnExitAttack();
                break;
            case EnnemyState.HURT:
                OnExitHurt();
                break;
            case EnnemyState.DEAD:
                OnExitDead();
                break;
            default:
                Debug.LogError("OnStateExit: Invalid state " + state.ToString());
                break;
        }
    }
    public void TransitionToState(EnnemyState toState)
    {
        OnStateExit(CurrentState);
        CurrentState = toState;
        OnStateEnter(toState);
    }

    #endregion

    #region State TARGET

    private void OnEnterTarget()
    {
        _ennemyController.StartPatrol();

    }
    private void OnUpdateTarget()
    {
        
        _ennemyController.DoPatrol();
        if (_ennemyController.IsPlayerNear)
        {
            _animator.SetTrigger("IsAttacking");
            TransitionToState(EnnemyState.ATTACK);
        }

    }
    private void OnFixedUpdateTarget()
    {
    }
    private void OnExitTarget()
    {
    }

    #endregion

    #region State ATTACK

    private void OnEnterAttack()
    {
    }
    private void OnUpdateAttack()
    {

        _ennemyController.DoAttack();

        if (_ennemyController.IsAttackEnded || !_ennemyController.IsPlayerNear)
        {
            TransitionToState(EnnemyState.TARGET);
        }

    }
    private void OnFixedUpdateAttack()
    {
    }
    private void OnExitAttack()
    {
    }

    #endregion

    #region State HURT

    private void OnEnterHurt()
    {
        _ennemyController.StartHurt();
        _animator.SetBool("IsHurting", true);
    }
    private void OnUpdateHurt()
    {
        if(_ennemyController.IsEndHurting)
        {
            if(_ennemyManager.Currenthealth > 0)
            {
                TransitionToState(EnnemyState.TARGET);
                return;
            }
            else
            {
                TransitionToState(EnnemyState.DEAD);
                return;
            }

        }
        _ennemyController.DoHurt();
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
        _animator.SetBool("IsDead", true);
    }
    private void OnUpdateDead()
    {
    }
    private void OnFixedUpdateDead()
    {
    }
    private void OnExitDead()
    {
        _animator.SetBool("IsDead", false);
    }

    #endregion

}
