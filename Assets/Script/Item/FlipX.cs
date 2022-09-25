using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 自分のY軸を180回したいアイテムにつける
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
