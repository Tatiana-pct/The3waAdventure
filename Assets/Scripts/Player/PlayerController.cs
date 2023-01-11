using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Reference settings

    //Speed settings
    [Header("Speed Settings")]
    [Space(25)]

    //Walk
    [SerializeField]
    [Tooltip("Vitesse de marche")]
    private float _moveSpeed = 2.5f;

    //Sprint
    [Space(1)]
    [SerializeField]
    [Tooltip("Vitesse de course")]
    private float _sprintSpeed = 5f;

    //Dash
    [Space(1f)]
    [SerializeField]
    [Tooltip("Vitesse du dash")]
    private float _dashSpeed = 25f;
    [SerializeField] float _dashTime = 0.2f;
    private float _dashCounter;
    [SerializeField] SpriteRenderer _spriteRenderer;
    [SerializeField] SpriteRenderer _afterImage;
    [SerializeField] float _afterImageLifeTime = 0.1f;
    [SerializeField] float _timeBetweenAfterImage = 0.03333334f;
    private float _afterImageCounter;
    [SerializeField] Color _afterImageColor;

    //Jump settings
    [SerializeField] Transform _groundPoint;

    //Simple Jump
    [Header("Jump Settings")]
    [SerializeField]
    [Tooltip("Hauteur du saut")]
    private float _JumpForce = 7f;


    [SerializeField]
    [Tooltip("Duree du saut")]
    private float _jumpDuration = 0.2f;


    [SerializeField]
    [Tooltip("Duree de l'atterissage")]
    private float _landingDuration = 0.2f;

    //DoubleJump
    private bool _canDoubleJump;

    //Attaque box
    [SerializeField]
    [Tooltip("Référence à l'AttackBox")]
    private GameObject _attackBox;

    //Interact
    PlayerCollision _playerCollision;


    //Health
    [SerializeField] float _hurtTimer;

    //reference aux composants
    private Rigidbody2D _rigidbody;
    private Animator _animator;
    private PlayerInput _playerInput;
    private Transform _transform;

    [SerializeField] LayerMask _whatIsGround;
    [SerializeField] LayerMask _whatIsHitable;

    [SerializeField] Transform _attackPoint;

    [SerializeField] PlayerManager _playerManager;


    //Variable
    private float _endDashing; // heure de fin du Dash
    private float _endJumping; // heure de fin de Saut
    private float _endLanding; // heure de fin de l'atterissage
    private float _endAttack; //heure de fin de l'attaque
    private bool _isOnGround;
    private int _jumpCount = 2;
    private float _endHurting;
    //private bool _candash;
    private Vector2 _lastDirection;

    //Ability tracker
    private PlayerAbilityTracker _PlayerAbilities;



    //Propriete
    public bool IsEndDashing { get => Time.time >= _endDashing; }
    public bool IsEndJumping { get => Time.time >= _endJumping; }
    public bool IsEndLanding { get => Time.time >= _endLanding; }
    public bool IsEndAttack { get => Time.time >= _endAttack; }
    public bool IsEndHurting { get => Time.time >= _endHurting; }


    public bool IsOnGround { get => _isOnGround; }
    public bool CanJump { get => _jumpCount > 0; }

    public bool IsHurt { get; set; }


    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody2D>();
        _animator = GetComponentInChildren<Animator>();
        _playerInput = GetComponent<PlayerInput>();
        _transform = GetComponent<Transform>();
        _playerCollision = GetComponent<PlayerCollision>();
        _PlayerAbilities = GetComponentInChildren<PlayerAbilityTracker>();
    }



    // Start is called before the first frame update
    void Start()
    {
        _playerManager.MaxHealth = _playerManager.MaxHealth;
        _playerManager.CurrentLifeCount = _playerManager.MaxlifeCount;
    }

    #region IDLE
    public void Idle()
    {
        _rigidbody.velocity = Vector3.zero;
    }
    #endregion

    #region WALK
    public void Walk()
    {
        _rigidbody.velocity = new Vector2(_playerInput.Move * _moveSpeed, _rigidbody.velocity.y);
        TurnCharacter();
    }
    #endregion

    #region SPRINT

    public void Sprint()
    {
        
            _rigidbody.velocity = new Vector2(_playerInput.Move * _sprintSpeed, _rigidbody.velocity.y);
            TurnCharacter();
        

    }

    #endregion

    #region TURN CHARACTER
    public void TurnCharacter()
    {
        if (_playerInput.Move < 0)
        {
            _transform.GetChild(0).right = Vector2.left;
        }
        else if (_playerInput.Move > 0)
        {
            _transform.GetChild(0).right = Vector2.right;
        }

    }
    #endregion

    #region DASH
    public void StartDash()
    {
        _endDashing = Time.time + _dashTime;
    }

    public void Dash()
    {
        //Vector2 veloc = _lastDirection * _dashSpeed;

        
            _rigidbody.velocity = _dashSpeed * _transform.GetChild(0).right;
            ShowAfterImage();
        
        
       

    }

    public void ShowAfterImage()
    {

        SpriteRenderer image = Instantiate(_afterImage, _spriteRenderer.transform.position, transform.rotation);
        image.sprite = _spriteRenderer.sprite;
        image.transform.rotation = _spriteRenderer.transform.rotation;
        image.color = _afterImageColor;

        Destroy(image.gameObject, _afterImageLifeTime);

        _afterImageCounter = _timeBetweenAfterImage;
    }
    #endregion

    #region JUMP
    public void StartJump()
    {
        _endJumping = Time.time + _jumpDuration;
        _jumpCount--;

    }

    public void Jump()
    {
        _rigidbody.velocity = Vector2.up * _JumpForce;

    }
    #endregion

    #region Land
    public void StartLand()
    {
        _endLanding = Time.time + _landingDuration;
    }

    public void DoLand()
    {
        _landingDuration = Time.time;
    }

    #endregion

    #region Ground
    public void Gound()
    {
        _jumpCount = 2;

    }

    #endregion

    #region ATTACK
    public void StartAttack()
    {
        //_attackBox.SetActive(true);
        _endAttack = Time.time + _playerManager.AttackTime;
    }

    private void DoAttack()
    {
        Collider2D hit = Physics2D.OverlapCircle(_attackPoint.position, 0.5f, _whatIsHitable);
        if (hit)
        {
            DoHit(hit);
        }
    }
    #endregion



    #region HEALTH

    public void DoHurt()
    {
        // On calcule la durée actuelle de la roulade
        float hurtTime = Time.time - (_endHurting - _hurtTimer);
        // On calcule la progression de la roulade en divisant la durée actuelle par la durée totale
        float hurtProgress = hurtTime / _hurtTimer;


        _animator.SetFloat("HurtProgress", hurtProgress);
    }

    public void StartHurt()
    {
        _endHurting = Time.time + _hurtTimer;
        _playerManager.Currenthealth--;
    }

    #endregion

    #region HIT
    public void DoHit(Collider2D hit)
    {


    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackBoxEnnemi"))
        {
            IsHurt = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("AttackBoxEnnemi"))
        {
            IsHurt = false;
        }
    }

    #endregion

    #region DEAD

    public void DoDead()
    {
        _playerManager.CurrentLifeCount--;
        _rigidbody.velocity = Vector3.zero;

    }

    #endregion







    private void FixedUpdate()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _isOnGround = Physics2D.OverlapCircle(_groundPoint.position, .2f, _whatIsGround);
    }


}
