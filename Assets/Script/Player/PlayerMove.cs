using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;
/// <summary>
/// プレイヤーの移動に関するもの
/// </summary>
public class PlayerMove : MonoBehaviour
{

    [Header("動き関係")]
    [Header("ジャンプ")]
    [Tooltip("ジャンプ力"), SerializeField] float _jumpPower = 40f;
    [SerializeField] private int _jumpChecker = 0;
    [SerializeField] bool _groundCheck;
    [SerializeField] bool _rightWallCheck;
    [SerializeField] bool _leftWallCheck;

    [Header("左右移動")]
    [SerializeField, Tooltip("現在速度")] private float _speed;
    [SerializeField, Tooltip("通常速度")] private float _defaultSpeed = 5f;
    [Tooltip("スロウ速度")] private float _slowSpeed = default;
    [Tooltip("滑った速度")] private float _splitSpeed = default;

    [Header("速度制限")]
    [SerializeField, Tooltip("速度制限")] private float _walkSpeedLimiter = 30f;
    [Tooltip("左右の速度")] private float _horizonSpeedLimiter;
    [Tooltip("上下の速度")] private float _jumpSpeedLimiter;
    [Header("ターンは把握用")]
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameManager _gameManager;

    [Header("当たり判定")]
    [SerializeField, Tooltip("ジャンプ用の当たり判定レイヤー！")] private LayerMask levelMask;

    [Header("入力ボタンの名前")]
    [SerializeField] string _jump;
    [SerializeField] string _horizontal;

    [Header("見たいだけ、今の状態")]
    [SerializeField][Tooltip("体力")] private int _hp = default;
    public DeBuff _deBuff = DeBuff.Default;
    public GameManager.Turn Turn;

    [Header("ポイント関係")]
    public PlayerState.GetScore Score;
    [SerializeField, Tooltip("ゴールに自分を渡したい")] GameObject _goal;
    [SerializeField] public int _scorePoint;
    [Header("音とアニメーション")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipJump;
    [SerializeField] AudioClip _audioClipDamage;
    [SerializeField] AudioClip _audioClipCoin;
    [Header("自分を入れる")]
    [SerializeField][Tooltip("自分の動きonoffするため")] PlayerMove controller;

    Rigidbody2D _rb = default;
    bool isreturn = false;

    //[Tooltip("走れるかどうかチェック")] bool _dashCheck;
    void SpeedController()
    {
        switch (_deBuff)
        {
            case DeBuff.Default:
                _horizonSpeedLimiter = _walkSpeedLimiter;
                break;
            case DeBuff.Split:
                _horizonSpeedLimiter = _walkSpeedLimiter * 2;
                break;
            case DeBuff.Slow:
                _horizonSpeedLimiter = _walkSpeedLimiter / 2;
                break;
        }
        _jumpSpeedLimiter = 50f;
    }
    void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
        _rb = GetComponent<Rigidbody2D>();
        _speed = _defaultSpeed;
        _slowSpeed = _defaultSpeed / 2;
        _splitSpeed = _defaultSpeed * 2;
    }
    void Update()
    {
        SpeedController();
        TurnChecker();
        if (Turn == GameManager.Turn.GamePlay)
        {
            bool jump = Input.GetButtonDown(_jump);
            if (jump)
            {
                Debug.Log(_horizontal);
                if (_jumpChecker == 1 && _groundCheck)
                {
                    _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                //壁じゃん
                if (_jumpChecker == 1 && !_groundCheck && _rightWallCheck)
                {
                    _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                //壁じゃん
                if (_jumpChecker == 1 && !_groundCheck && _leftWallCheck)
                {
                    _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
            }
        }
        RightWallCheker(Vector3.right); // 右に広げる
        LeftWallCheker(Vector3.left); // 左に広げる
        GroundCheker(Vector3.down); // 下に広げる

        //自分の速度を変える所
        if (_deBuff == DeBuff.Default)
        {
            _speed = _defaultSpeed;
        }
        if (_deBuff == DeBuff.Slow)
        {
            _speed = _slowSpeed;
        }
        if (_deBuff == DeBuff.Split)
        {
            _speed = _splitSpeed;
        }
        if (_rightWallCheck || _leftWallCheck)
        {
            _rb.AddForce(Vector2.down * 0.3f, ForceMode2D.Impulse);
        }
    }
    private void FixedUpdate()
    {
        TurnChecker();
        if (Turn == GameManager.Turn.GamePlay)
        {
            float horizontalKey = Input.GetAxis(_horizontal);
            bool TF = horizontalKey != 0 ? true : false;
            animator.SetBool("Horizontal", TF);
            FlipX(horizontalKey);
            Debug.Log(gameObject.name);
            //右入力で左向きに動く
            if (horizontalKey > 0)
            {
                _rb.AddForce(Vector2.right * _speed, ForceMode2D.Impulse);
            }
            //左入力で左向きに動く
            else if (horizontalKey < 0)
            {
                _rb.AddForce(Vector2.left * _speed, ForceMode2D.Impulse);
            }
            //落下速度制限
            if (_rb.velocity.y < -_jumpSpeedLimiter)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_jumpSpeedLimiter);
            }
            //壁に触れた時落下速度が遅くなる
            if (_rightWallCheck || _leftWallCheck)
            {
                if (_rb.velocity.y < -_jumpSpeedLimiter / 2)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, -_jumpSpeedLimiter / 2);
                }
            }
            //速度制限書いたところ
            if (_rb.velocity.y > _jumpSpeedLimiter)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpSpeedLimiter);
            }
            if (_rb.velocity.x > _horizonSpeedLimiter)
            {
                _rb.velocity = new Vector2(_horizonSpeedLimiter, _rb.velocity.y);
            }
            if (_rb.velocity.x < -_horizonSpeedLimiter)
            {
                _rb.velocity = new Vector2(-_horizonSpeedLimiter, _rb.velocity.y);
            }
        }
    }
    /// <summary>
    /// ダメージ受ける用
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
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
        //自分をコイン取得状態にする
        if (collision.gameObject.CompareTag("Coin"))
        {
            _audioSource.PlayOneShot(_audioClipCoin);
            collision.gameObject.tag = "isUsed";
            Score |= PlayerState.GetScore.Coin;
            Debug.Log(Score);
        }
        //自分をゴール状態にする
        if (collision.gameObject.name == "Goal")
        {
            Score |= PlayerState.GetScore.isGoal;
            Debug.Log(Score);
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        _jumpChecker = 1;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        _jumpChecker = 0;
    }
    /// <summary>
    /// 自分の絵を左右反転させる
    /// </summary>
    /// <param name="horizontal"></param>
    public void FlipX(float horizontal)
    {
        if (horizontal > 0)
        {
            this.transform.localScale = new Vector3(Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            isreturn = false;
        }
        else if (horizontal < 0)
        {
            this.transform.localScale = new Vector3(-1 * Mathf.Abs(this.transform.localScale.x), this.transform.localScale.y, this.transform.localScale.z);
            isreturn = true;
        }
    }

    /// <summary>
    /// 右にレイ飛ばしてウォールチェックをonoffする、できてない
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void RightWallCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // 左右に何も存在しない場合
            if (!hit.collider)
            {
                _rightWallCheck = false;
            }
            // 左右にブロックが存在する場合
            else
            {
                _rightWallCheck = true;
            }
        }
    }

    /// <summary>
    /// 左にレイ飛ばしてウォールチェックをonoffする、できてない
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void LeftWallCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // 左右に何も存在しない場合
            if (!hit.collider)
            {
                _leftWallCheck = false;
            }
            // 左右にブロックが存在する場合
            else
            {
                _leftWallCheck = true;
            }
        }
    }

    /// <summary>
    /// 足元にレイ飛ばしてグラウンドチェックをonoffする、できてない
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void GroundCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // 下に何も存在しない場合
            if (!hit.collider)
            {
                _groundCheck = false;
            }
            // 下にブロックが存在する場合
            else
            {
                _groundCheck = true;
            }
        }
    }
    void TurnChecker()
    {
        Turn = _gameManager.NowTurn;
    }
}