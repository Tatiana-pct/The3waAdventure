/// <summary>
/// Script de reference de la Classe de quete de collect de pickup de capacité
/// </summary>
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LearnAbilities
{
    [SerializeField] string _title;
    [SerializeField] string _description;
    //[SerializeField] int _gold;
    //[SerializeField] int _xp;
    [SerializeField] bool _isActive;
    [SerializeField] bool _isCompleted = false;
    [SerializeField] string _obj;
    [SerializeField] GameObject _pickup;
    [SerializeField] int _reqAmount; 
    [SerializeField] int _reqCount = 0;


    //methode qui incremente le nbr d'objet requis pour la quete en cours
    public void IncrementCount()
    {

        _reqCount++;
        if( _reqCount >= _reqAmount )
        {
            _isCompleted = true;    
        }
    }

    public string Title { get => _title; set => _title = value; }
    public string Description { get => _description; set => _description = value; }
    public bool IsActive { get => _isActive; set => _isActive = value; }
    public bool IsCompleted { get => _isCompleted; set => _isCompleted = value; }
    public int ReqCount { get => _reqCount; set => _reqCount = value; }
    public GameObject Pickup { get => _pickup; set => _pickup = value; }
}
