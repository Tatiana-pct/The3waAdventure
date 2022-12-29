using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Parallaxe : MonoBehaviour
{
     float _lenght;
     float _startPos;
    [SerializeField] Camera _camera;
    [SerializeField] float _parrallaxeEffect;

    // Start is called before the first frame update
    void Start()
    {
        _startPos = transform.position.x;
        _lenght = GetComponent<SpriteRenderer>().bounds.size.x;
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        float distance = (_camera.transform.position.x * _parrallaxeEffect);
        transform.position = new Vector3 (_startPos + distance, transform.position.y, transform.position.z);
    }
}
