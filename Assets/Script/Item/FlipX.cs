using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlipX : MonoBehaviour
{
    public bool _flipX;
    private void Start()
    {

    }
    void Update()
    {
        if (_flipX)
        {
            gameObject.transform.Rotate(0, gameObject.transform.rotation.y + 180, 0);
            _flipX = false;
        }

    }
}
