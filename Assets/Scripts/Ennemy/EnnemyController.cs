using Unity.VisualScripting;
using UnityEngine;

public class EnnemyController : MonoBehaviour
{
    [Header("EnnemySetting")]
    [SerializeField] EnnemyManager _ennemyManager;
    [SerializeField] Transform _skelletontranform;

    [Header("EnnemyPatroller")]
    [SerializeField] Transform[] _patrolsPoints;
    private int _currentPoint;
    [SerializeField] float _moveSpeed;
    [SerializeField] float _waitAPoints;
    private float _waitCounter;
    [SerializeField] float _jumpForce;


    [Header("EnnemyAttack")]
    [SerializeField] float _attackCoolDown;
    [SerializeField] LayerMask _WhatIsThePlayer;
    GameObject _otherObject;
    [SerializeField]GameObject _attackBox;

    //Health
    [SerializeField] float _hurtTimer;


    private float _coolDownTimer;
    Transform _transform;
    Animator _animator;
    Rigidbody2D _rigidbody2D;

    //Timer
    float _patrolEndTime;
    float _attackEndTime;
    private float _endHurting;



    // public bool IsAttackEnded { get { return Time.time >= _attackEndTime; } }
    public bool IsPatrolEnded { get { return Time.time >= _patrolEndTime; } }

    public bool IsAttackEnded { get { return Time.time >= _coolDownTimer; } }

    public bool IsWaitCounter { get { return Time.time >= _waitAPoints; } }

    public bool IsPlayerNear { get { return _otherObject != null; } }

    public bool IsEndHurting { get => Time.time >= _endHurting; }

    public bool IsHurt { get; set; }

    // Start is called before the first frame update
    void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
        _rigidbody2D = GetComponentInChildren<Rigidbody2D>();
    }
    private void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
      
    }

    #region PATROL CONTROLLER
    public void StartPatrol()
    {
        _waitCounter = _waitAPoints;
        foreach (Transform pPoint in _patrolsPoints)
        {
            pPoint.SetParent(null);
        }
    }

    public void DoPatrol()
    {
        if (Mathf.Abs(transform.position.x - _patrolsPoints[_currentPoint].position.x) > .1f)
        {
            if (transform.position.x < _patrolsPoints[_currentPoint].position.x)
            {
                _rigidbody2D.velocity = new Vector2(_moveSpeed, _rigidbody2D.velocity.y);
                _skelletontranform.right = Vector3.right;

            }
            else
            {
                _rigidbody2D.velocity = new Vector2(-_moveSpeed, _rigidbody2D.velocity.y);
                _skelletontranform.right = Vector3.left;
            }
            if (transform.position.y < _patrolsPoints[_currentPoint].position.y - .5f && _rigidbody2D.velocity.y < .1f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            _waitCounter -= Time.deltaTime;
            if (_waitCounter <= 0)
            {
                _waitCounter = _waitAPoints;
                _currentPoint++;

                if (_currentPoint >= _patrolsPoints.Length)
                {
                    _currentPoint = 0;
                }
            }
        }
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));

    }


    #endregion


    #region ATTACKING CONTROLLER
    
    public void DoAttack()
    {
        
        //attack only player in sight?
        if (_otherObject != null)
        {
            
            if (Time.time > _coolDownTimer)
            {
                //Attack
                _coolDownTimer = Time.time + _attackCoolDown;
                _animator.SetTrigger("IsAttacking");
                
            }
        }
       
    }
   
    #endregion

    #region HURTING CONTROLLER

     public void DoHurt()
    {
        float hurtTime = Time.time - (_endHurting - _hurtTimer);
        float hurtProgress = hurtTime / _hurtTimer;
        _animator.SetFloat("HurtProgress", hurtProgress);
    }
    public void StartHurt()
    {
        _endHurting = Time.time + _hurtTimer;
        _ennemyManager.Currenthealth--;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            
            _otherObject = collision.gameObject;
        }
        
        if (collision.CompareTag("AttackBoxPlayer"))
        {
            IsHurt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _otherObject = null;
        }

        if (collision.CompareTag("AttackBoxPlayer"))
        {
            IsHurt = false;
        }

    }
    

   


    #endregion

    #region

    #endregion



}
