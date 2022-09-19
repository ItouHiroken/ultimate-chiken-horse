using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField][Tooltip("”­ŽËŽžŠÔ‚ð0•b‚É–ß‚·—p‚Ì‚â‚Â")] private float _targetTime = default;
    [SerializeField][Tooltip("”­ŽË‚·‚éŠÔŠu")] private float _currentTime = default;
    [SerializeField]private bool isReturn = false;
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_targetTime < _currentTime)
        {
            
        }
    }
}
