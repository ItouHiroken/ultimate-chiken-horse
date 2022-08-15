using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[�̈ړ��Ɋւ������
/// </summary>
public class Player1Move : PlayerBase
{ 
    [SerializeField][Tooltip("�̗�")] private int _hp = default;
    [Tooltip("����邩�ǂ����`�F�b�N")] bool _dashCheck;
    [SerializeField] private float _horizonSpeedLimiter;
    [SerializeField] private float _jumpSpeedLimiter;
    [SerializeField][Tooltip("�����̓���onoff���邽��")] Player1Move controller;
    private Player1State.GetScore Score;

    protected override void SpeedController()
    {
        _horizonSpeedLimiter = WalkSpeedLimiter;
        _jumpSpeedLimiter = 50f;
    }

    protected new void Update()/////��������������������������new����ƂȂɂ����\���ɂ���炵���A�Ȃɂ��Ȃ�Ȃ̂��킩��Ȃ����畷��
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashCheck = !_dashCheck;
        }
        bool jump = Input.GetButtonDown("Jump")|| Input.GetKeyDown("joystick button 1");
        if (jump==true )
        {
            if (JumpChecker == 1 && GroundCheck)
            {
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
            if (JumpChecker == 1 && !GroundCheck && RightWallCheck)
            {
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);

            }
            if (JumpChecker == 1 && !GroundCheck && LeftWallCheck)
            {
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
            if (_dashCheck == true)
            {
                _horizonSpeedLimiter = RunSpeedLimiter;
            }
        }
        //�����͂ō������ɓ���
        else if (horizontalKey < 0)
        {
            Rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
            if (_dashCheck == true)
            {
                _horizonSpeedLimiter = RunSpeedLimiter;
            }
        }
        if (_dashCheck == false)
        {
            _horizonSpeedLimiter = WalkSpeedLimiter;
        }
        if (Rb.velocity.y < -_jumpSpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -_jumpSpeedLimiter);
        }

        if (RightWallCheck || LeftWallCheck)
        {
            if (Rb.velocity.y < -_jumpSpeedLimiter / 2)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, -_jumpSpeedLimiter / 2);
            }
        }
        if (Rb.velocity.y > _jumpSpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, _jumpSpeedLimiter);
        }
        if (Rb.velocity.x > _horizonSpeedLimiter)
        {
            Rb.velocity = new Vector2(_horizonSpeedLimiter, Rb.velocity.y);
        }
        if (Rb.velocity.x < -_horizonSpeedLimiter)
        {
            Rb.velocity = new Vector2(-_horizonSpeedLimiter, Rb.velocity.y);
        }
    }
    /// <summary>
    /// �_���[�W�󂯂�p
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageController damage))
        {
            _hp -= damage.Damage;
            if (_hp <= 0)
            {
                controller.enabled = false;
                Score = Score | Player1State.GetScore.Death;
                Score &= ~Player1State.GetScore.Default;
                Debug.Log(Score);
            }
        }
        if (collision.gameObject.tag == "Coin")
        {
            Score = Score | Player1State.GetScore.Coin;
            Debug.Log(Score);
        }
        if (collision.gameObject.name == "Goal")
        {
            Score = Score | Player1State.GetScore.isGoal;
            Debug.Log(Score);
        }
    }
}
