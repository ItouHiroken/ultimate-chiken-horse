using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;
/// <summary>
/// �v���C���[�̈ړ��Ɋւ������
/// </summary>
public class PlayerMove : PlayerBase
{
    [SerializeField] string _jump;
    [SerializeField] string _horizontal;
    [SerializeField][Tooltip("�̗�")] private int _hp = default;
    [Tooltip("����邩�ǂ����`�F�b�N")] bool _dashCheck;
    [SerializeField] private float _horizonSpeedLimiter;
    [SerializeField] private float _jumpSpeedLimiter;
    [SerializeField][Tooltip("�����̓���onoff���邽��")] PlayerMove controller;
    public PlayerState.GetScore Score;
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;

    [SerializeField] public int _scorePoint;
    [SerializeField] Animator animator;
    protected override void SpeedController()
    {
        _horizonSpeedLimiter = WalkSpeedLimiter;
        _jumpSpeedLimiter = 50f;
    }

    protected new void Update()
    {
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //����������
                    Debug.Log(code);
                    break;
                }
            }
        }
        ////
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.GamePlay)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _dashCheck = !_dashCheck;
            }
            bool jump = Input.GetButtonDown(_jump);
            if (jump)
            {
            Debug.Log(_horizontal);
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
    }
    private void FixedUpdate()
    {
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.GamePlay)
        {
            float horizontalKey = Input.GetAxis(_horizontal);
            bool TF = horizontalKey != 0 ? true : false;
            animator.SetBool("Horizontal", TF);
            base.FlipX(horizontalKey);
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
                Score |= PlayerState.GetScore.Death;
                Score &= ~PlayerState.GetScore.Default;
                Debug.Log(Score);
            }
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.tag = "isUsed";
            Score |= PlayerState.GetScore.Coin;
            Debug.Log(Score);
        }
        if (collision.gameObject.name == "Goal")
        {
            Score |= PlayerState.GetScore.isGoal;
            Debug.Log(Score);
        }
    }
    void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}