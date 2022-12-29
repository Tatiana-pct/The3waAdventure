using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DialTaker : MonoBehaviour
{
    DialGiver _dialGiver;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "MentorDial")
        {
            _dialGiver = other.gameObject.GetComponent<DialGiver>();
            _dialGiver.DialPanel.SetActive(true);
            _dialGiver.DialtextInfo[0].text = _dialGiver.Dial.Title;
            _dialGiver.DialtextInfo[1].text = _dialGiver.Dial.Content;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "MentorDial")
        {
            
            _dialGiver.DialPanel.SetActive(false);
            _dialGiver = null;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
