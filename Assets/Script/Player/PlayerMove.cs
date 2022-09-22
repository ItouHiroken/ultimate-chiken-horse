using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;
/// <summary>
/// プレイヤーの移動に関するもの
/// </summary>
public class PlayerMove : PlayerBase
{
    [Header("入力ボタンの名前")]
    [SerializeField] string _jump;
    [SerializeField] string _horizontal;

    [Header("見たいだけ")]
    [SerializeField][Tooltip("体力")] private int _hp = default;
    [SerializeField][Tooltip("左右の速度")] private float _horizonSpeedLimiter;
    [SerializeField][Tooltip("上下の速度")] private float _jumpSpeedLimiter;

    [Header("自分を入れる")]
    [SerializeField][Tooltip("自分の動きonoffするため")] PlayerMove controller;

    [Header("ターンは把握用")]
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;

    [Header("ポイント関係")]
    public PlayerState.GetScore Score;
    [SerializeField, Tooltip("ゴールに自分を渡したい")] GameObject _goal;
    [SerializeField] public int _scorePoint;

    [Header("音とアニメーション")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipJump;
    [SerializeField] AudioClip _audioClipDamage;
    //[Tooltip("走れるかどうかチェック")] bool _dashCheck;
    void SpeedController()
    {
        switch (_deBuff)
        {
            case DeBuff.Default:
                _horizonSpeedLimiter = WalkSpeedLimiter;
                break;
            case DeBuff.Split:
                _horizonSpeedLimiter = WalkSpeedLimiter * 2;
                break;
            case DeBuff.Slow:
                _horizonSpeedLimiter = WalkSpeedLimiter / 2;
                break;
        }
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
                    //処理を書く
                    Debug.Log(code);
                    break;
                }
            }
        }
        ////
        SpeedController();
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.GamePlay)
        {
            base.Update();
            //if (Input.GetKeyDown(KeyCode.LeftShift))
            //{
            //    _dashCheck = !_dashCheck;
            //}
            bool jump = Input.GetButtonDown(_jump);
            if (jump)
            {
                Debug.Log(_horizontal);
                if (JumpChecker == 1 && GroundCheck)
                {
                    Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                if (JumpChecker == 1 && !GroundCheck && RightWallCheck)
                {
                    Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    Rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                if (JumpChecker == 1 && !GroundCheck && LeftWallCheck)
                {
                    Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    Rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
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
            Debug.Log(gameObject.name);
            //右入力で左向きに動く
            if (horizontalKey > 0)
            {
                Rb.AddForce(Vector2.right * Speed, ForceMode2D.Impulse);
                //if (_dashCheck == true)
                //{
                //    _horizonSpeedLimiter = RunSpeedLimiter;
                //}
            }
            //左入力で左向きに動く
            else if (horizontalKey < 0)
            {
                Rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
                //if (_dashCheck == true)
                //{
                //    _horizonSpeedLimiter = RunSpeedLimiter;
                //}
            }
            //if (_dashCheck == false)
            //{
            //    _horizonSpeedLimiter = WalkSpeedLimiter;
            //}
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
    /// ダメージ受ける用
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageController damage))
        {
            _hp -= damage.Damage;
            if (_hp <= 0)
            {
                _audioSource.PlayOneShot(_audioClipDamage);
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