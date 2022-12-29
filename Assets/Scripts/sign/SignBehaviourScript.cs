using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SignBehaviourScript : MonoBehaviour
{
    [SerializeField] string _signTexte;
    [SerializeField] GameObject _uI;
    [SerializeField] GameObject _textCanvas;
    [SerializeField] TMP_Text _tmpText;


    public string SignTexte { get => _signTexte; }
    public GameObject UI { get => _uI;}
    public GameObject TextCanvas { get => _textCanvas; }
    public TMP_Text TmpText { get => _tmpText;}
}
