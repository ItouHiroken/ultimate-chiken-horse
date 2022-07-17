using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[�̈ړ��Ɋւ������
/// </summary>
public class Player1Move : PlayerBase
{
    [SerializeField][Tooltip("�_�b�V���R�}���h����")] private float _targetTime = default;
    [SerializeField][Tooltip("�_�b�V���`�F�b�N���Ԃ�0�b�ɖ߂��p�̂��")] private float _currentTime = default;
    [Tooltip("����邩�ǂ����`�F�b�N")] bool _dashCheck;
    [SerializeField] private float _speedLimiter;
    protected override void SpeedController()
    {
        _speedLimiter = WalkSpeedLimiter;
    }
    private void Update()
    {
        _currentTime += Time.deltaTime;
        if (_currentTime <= _targetTime)
        {
            _dashCheck = true;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            if (JumpChecker == 1 && GroundCheck)
            {
                Debug.Log("a");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
            if (JumpChecker == 1 && !GroundCheck && WallCheck && MyPosition > StartPosition)
            {
                Debug.Log("ue");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);

            }
            if (JumpChecker == 1 && !GroundCheck && WallCheck && MyPosition < StartPosition)
            {
                Debug.Log("shita");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
            }
        }
    }
    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        //�E���͂ō������ɓ���
        if (horizontalKey > 0)
        {
            Rb.AddForce(Vector2.right * Speed, ForceMode2D.Impulse);
            _currentTime = 0;
            if (_dashCheck == true)
            {
                _speedLimiter = RunSpeedLimiter;
            }
        }
        //�����͂ō������ɓ���
        else if (horizontalKey < 0)
        {
            Rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
            _currentTime = 0;
            if (_dashCheck == true)
            {
                _speedLimiter = RunSpeedLimiter;
            }
        }
        if (horizontalKey == 0)
        {
            _speedLimiter = WalkSpeedLimiter;
        }
        if (Rb.velocity.y < -_speedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -_speedLimiter);
        }

        if (WallCheck)
        {
            if (Rb.velocity.y < -_speedLimiter / 2)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, -_speedLimiter / 2);
            }
        }
        if (Rb.velocity.y > _speedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, _speedLimiter);
        }
        if (Rb.velocity.x > _speedLimiter)
        {
            Rb.velocity = new Vector2(_speedLimiter, Rb.velocity.y);
        }
        if (Rb.velocity.x < -_speedLimiter)
        {
            Rb.velocity = new Vector2(-_speedLimiter, Rb.velocity.y);
        }
    }
}

