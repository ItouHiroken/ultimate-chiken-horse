using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField][Tooltip("発射時間を0秒に戻す用のやつ")] private float _targetTime = default;
    [SerializeField][Tooltip("発射する間隔")] private float _currentTime = default;
    [SerializeField]private bool isreturn = false;
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_targetTime < _currentTime)
        {
            
        }
    }
}
