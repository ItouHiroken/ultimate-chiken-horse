using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour,IPause
{
    [SerializeField][Tooltip("���ˎ��Ԃ�0�b�ɖ߂��p�̂��")] private float _targetTime = default;
    [SerializeField][Tooltip("���˂���Ԋu")] private float _currentTime = default;
    [SerializeField]private bool isreturn = false;
    void Update()
    {
        _currentTime += Time.deltaTime;
        if (_targetTime < _currentTime)
        {
            
        }
    }
    void IPause.Pause()
    {

    }
    void IPause.Resume()
    {
    }
}
