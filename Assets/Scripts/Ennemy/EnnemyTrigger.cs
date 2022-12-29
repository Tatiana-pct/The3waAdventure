using UnityEngine;
using UnityEngine.UI;


public class EnnemyTrigger : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
   
    [SerializeField] GameObject _player;
    Animator _animator;


    private void Awake()
    {
       
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _animator = GetComponentInChildren<Animator>();
    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
          
            _playerManager.Currenthealth--;

            if (_playerManager.Currenthealth == 0)
            {
                
                //Destroy(collision.gameObject);
                Debug.Log("Player DEAD!");
            }
        }
    }
}
