/// <summary>
/// Script des pickups de capacitées
/// </summary>
using UnityEngine;
using TMPro;

public class AbilityUnlock : MonoBehaviour
{
    [SerializeField] bool _unlockDash, _unlockAttack, _unlockSprint, _unlockJump;
    [SerializeField] GameObject _pickupEffect;
    [SerializeField] string _UnlockMessage;
    [SerializeField] TMP_Text _UnlockText;

   
    private void Awake()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Player")
        {
            PlayerAbilityTracker playerAbilityTracker = other.GetComponentInParent<PlayerAbilityTracker>();

            if(_unlockDash)
            {
                playerAbilityTracker.CanDash = true;
            }
            if (_unlockAttack)
            {
                playerAbilityTracker.CanAttack = true;
            }
            if (_unlockSprint)
            {
                playerAbilityTracker.CanSprint = true;
            }
            if (_unlockJump)
            {
                playerAbilityTracker.CanJump = true;
            }

            Instantiate(_pickupEffect, transform.position, transform.rotation);
            _UnlockText.transform.parent.SetParent(null);
            _UnlockText.transform.parent.position = transform.position;

            _UnlockText.text = _UnlockMessage;
            _UnlockText.gameObject.SetActive(true);

            Destroy(_UnlockText.transform.parent.gameObject, 3f);
            Destroy(gameObject);
        }
    }
}
