using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ������Y����180�񂵂����A�C�e���ɂ���
/// </summary>

public class FlipX : MonoBehaviour
{
    public bool _flipX;
    void Update()
    {
        if (_flipX)
        {
            gameObject.transform.Rotate(0, gameObject.transform.rotation.y + 180, 0);
            _flipX = false;
        }

    }
}
