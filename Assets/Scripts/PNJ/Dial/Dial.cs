using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dial 
{
    [SerializeField] string _title;
    [SerializeField] string _content;

    public string Title { get => _title; set => _title = value; }
    public string Content { get => _content; set => _content = value; }
}
