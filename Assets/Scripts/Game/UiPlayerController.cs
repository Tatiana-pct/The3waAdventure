using UnityEngine;
using UnityEngine.UI;

public class UiPlayerController : MonoBehaviour
{
    [SerializeField] PlayerManager _playerManager;
   

    Image _image;

    
    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _playerManager.Currenthealth = _playerManager.MaxHealth;
      

    }

    // Update is called once per frame
    void Update()
    {
       
        SetHealth();
    }



    public void SetHealth()
    {
        _image.fillAmount = (float)_playerManager.Currenthealth / _playerManager.MaxHealth;
        
    }
}
