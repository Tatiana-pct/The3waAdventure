using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialGiver : MonoBehaviour
{
    
    [Header("reference au script dial")]
    [SerializeField] Dial _dial;
    [SerializeField] GameObject _dialPanel;
    [SerializeField] TMP_Text[] _dialtextInfo;

    public Dial Dial { get => _dial; set => _dial = value; }
    public GameObject DialPanel { get => _dialPanel; set => _dialPanel = value; }
    public TMP_Text[] DialtextInfo { get => _dialtextInfo; set => _dialtextInfo = value; }
}
