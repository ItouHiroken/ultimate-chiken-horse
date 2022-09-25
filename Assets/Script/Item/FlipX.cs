using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Ž©•ª‚ÌYŽ²‚ð180‰ñ‚µ‚½‚¢ƒAƒCƒeƒ€‚É‚Â‚¯‚é
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
