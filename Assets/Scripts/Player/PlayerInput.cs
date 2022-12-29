using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour
{

    //Axe de deplacement
    

    private float _move;
    //Bouton Sprint
    private bool _sprint;
    // Bouton saut
    private bool _jump;
    //Bouton Dash
    private bool _dash;
    //Bouton Attack
    private bool _attack;
    //Bouton Interect
    private bool _interact;


    private PlayerController _playerController;

    //Getter | Setter
    public float Move { get => _move;}

    public bool HasMouvement { get { return _move != 0; } }
    
    public bool Sprint { get => _sprint;}

    public bool Jump { get => _jump;}

    public bool Dash { get => _dash; }
    public bool Attack { get => _attack;}
    public bool Interact { get => _interact;}





    // Start is called before the first frame update
    void Start()
    {
     _playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        _move = Input.GetAxisRaw("Horizontal");

        _sprint = Input.GetButton("Sprint");

        _dash = Input.GetButtonDown("Dash");

        _jump = Input.GetButtonDown("Jump");

        _attack = Input.GetKeyDown(KeyCode.Mouse0);

        _interact = Input.GetKeyDown(KeyCode.E);
    }




}
