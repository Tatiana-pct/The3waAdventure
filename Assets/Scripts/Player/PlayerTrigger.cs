using Unity.VisualScripting;
using UnityEngine;

public class PlayerTrigger : MonoBehaviour
{
    [SerializeField] EnnemyManager _ennemyManager;
    [SerializeField] GameObject _ennemy;
    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
       //_animator = GetComponentInParent<Animator>();
       // _animator = FindObjectOfType<EnnemyController>().GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ennemy"))
        {
            
            
            _ennemyManager.Currenthealth--;
             if(_ennemyManager.Currenthealth <= 0)
            {
               
                //_animator.SetTrigger("IsDead");
               
                Destroy(collision.gameObject);
            }

            
        }
    }

   


}
