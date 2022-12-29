using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAbilityTracker : MonoBehaviour
{
    [SerializeField] bool _canDash;
    [SerializeField] bool _canAttack;
    [SerializeField] bool _canSprint;
    [SerializeField] bool _canJump;
    public bool CanDash { get => _canDash; set => _canDash = value; }
    public bool CanAttack { get => _canAttack; set => _canAttack = value; }
    public bool CanSprint { get => _canSprint; set => _canSprint = value; }
    public bool CanJump { get => _canJump; set => _canJump = value; }





    //public bool CanDash { get => _canDash;}
    //public bool CanSprint { get => _canSprint;}
    //public bool CanJump { get => _canJump;}
}
