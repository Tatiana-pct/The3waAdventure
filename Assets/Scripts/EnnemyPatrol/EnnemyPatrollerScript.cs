using UnityEngine;

public class EnnemyPatrollerScript : MonoBehaviour
{
    [Header("EnnemyPatroller")]
    [Min(10)]
    [SerializeField] Transform[] _patrolsPoints;
    private int _currentPoint;

    [SerializeField] float _moveSpeed;
    [SerializeField] float _waitAPoints;

    private float _waitCounter;
    [SerializeField] float _jumpForce;
    [SerializeField] Rigidbody2D _rigidbody2D;
    private Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponentInChildren<Animator>();
        _waitCounter = _waitAPoints;
        foreach (Transform pPoint in _patrolsPoints)
        {
            pPoint.SetParent(null);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Mathf.Abs(transform.position.x - _patrolsPoints[_currentPoint].position.x) > .2f) 
        {
            if (transform.position.x < _patrolsPoints[_currentPoint].position.x)
            {
                _rigidbody2D.velocity = new Vector2(_moveSpeed, _rigidbody2D.velocity.y);
                transform.localScale = new Vector3(-1f, 1f, 1f);

            }
            else
            {
                _rigidbody2D.velocity = new Vector2(-_moveSpeed, _rigidbody2D.velocity.y);
                transform.localScale = Vector3.one;
            }
            if (transform.position.y < _patrolsPoints[_currentPoint].position.y -.5f && _rigidbody2D.velocity.y <.1f)
            {
                _rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, _jumpForce);
            }
        }
        else
        {
            _rigidbody2D.velocity = new Vector2(0f, _rigidbody2D.velocity.y);
            _waitCounter -= Time.deltaTime;
            if(_waitCounter <= 0)
            {
                _waitCounter = _waitAPoints;
                _currentPoint ++;

                if(_currentPoint >= _patrolsPoints.Length)
                {
                    _currentPoint = 0;
                }
            }
        }
        _animator.SetFloat("Speed", Mathf.Abs(_rigidbody2D.velocity.x));
        //_animator.SetBool("IsWalking", true);
    }
}
