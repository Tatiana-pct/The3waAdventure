using UnityEngine;
using UnityEngine.UI;

public class UiEnnemy : MonoBehaviour
{
    [SerializeField]EnnemyManager _ennemyManager;
    [SerializeField]RectTransform _healthRectTransform;


    Image _image;


    private void Awake()
    {
        _image = GetComponent<Image>();
    }
    // Start is called before the first frame update
    void Start()
    {
        _ennemyManager.Currenthealth = _ennemyManager.MaxHealth;


    }

    // Update is called once per frame
    void Update()
    {

        SetHealth();
    }



    public void SetHealth()
    {
        
        _image.fillAmount = (float)_ennemyManager.Currenthealth / _ennemyManager.MaxHealth;
        
    }
}
