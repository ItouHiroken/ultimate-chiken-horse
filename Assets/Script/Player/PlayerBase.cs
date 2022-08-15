using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player1State;
public abstract class PlayerBase : MonoBehaviour
{
    public DeBuff _deBuff = DeBuff.Default;
    /// <summary>左右移動する力</summary>
    [Tooltip("現在速度")][SerializeField] private float _speed;
    public float Speed { get { return _speed; } }
    [Tooltip("通常速度")] private float _defaultSpeed = 5f;
    public float DefaultSpeed { get { return _defaultSpeed; } }

    [Tooltip("スロウ速度")] private float _slowSpeed = default;
    public float SlowSpeed { get { return _slowSpeed; } }

    [Tooltip("滑った速度")] private float _splitSpeed = default;
    public float SplitSpeed { get { return _splitSpeed; } }

    [Tooltip("歩いた時の速度制限")][SerializeField] private float _walkSpeedLimiter = 30f;
    public float WalkSpeedLimiter { get { return _walkSpeedLimiter; } }

    [Tooltip("走るときの速度制限")][SerializeField] private float _runSpeedLimiter = default;
    public float RunSpeedLimiter { get { return _runSpeedLimiter; } }
    /// <summary>ジャンプする力</summary>
    [Tooltip("ジャンプ力")][SerializeField] float _jumpPower = 40f;
    public float JumpPower { get { return _jumpPower; } }
    /// <summary>水平方向の入力値</summary>
    float _horizontal = default;
    /// <summary>入力に応じて左右を反転させるかどうかのフラグ</summary>
    [SerializeField] bool _flipX = false;
    public object AddForce { get; private set; }

    public bool isreturn = false;

    [SerializeField] public Rigidbody2D _rb = default;
    [SerializeField] public Rigidbody2D Rb;

    [SerializeField] private int _jumpChecker = 0;
    public int JumpChecker { get { return _jumpChecker; } }

    [SerializeField] bool _groundCheck;
    public bool GroundCheck { get { return _groundCheck; } }
    [SerializeField] bool _rightWallCheck;
    public bool LeftWallCheck { get { return _rightWallCheck; } }
    [SerializeField] bool _leftWallCheck;
    public bool RightWallCheck { get { return _leftWallCheck; } }

    //public bool isDead;
   // public bool isGoal1 = false;
    [SerializeField][Tooltip("違うレイヤーで当たり判定とるよ！")] private LayerMask levelMask;

    GameManager gameManager;

    

    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        Rb = GetComponent<Rigidbody2D>();
        _speed = _defaultSpeed;
        _slowSpeed = _defaultSpeed / 2;
        _splitSpeed = _defaultSpeed * 2;
        _runSpeedLimiter = _walkSpeedLimiter * 2;
        SpeedController();
    }
    protected virtual void SpeedController()
    {
    }

    // Update is called once per frame
    protected void Update()
    {
        RightWallCheker(Vector3.right); // 右に広げる
        LeftWallCheker(Vector3.left); // 左に広げる
        GroundCheker(Vector3.down); // 下に広げる

        _horizontal = Input.GetAxisRaw("Horizontal");
        // 設定に応じて左右を反転させる
        if (_flipX)
        {
            FlipX(_horizontal);
        }
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
        if ((_deBuff & DeBuff.Slow) == DeBuff.Slow)
        {

        }
        if (RightWallCheck || LeftWallCheck)
        {
            Rb.AddForce(Vector2.down * 0.3f, ForceMode2D.Impulse);
        }
        if (gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            //ここで自分の事をoffにしたい
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        _jumpChecker = 1;
    }

    /// <summary>
    /// タグでオンオフさせてる、やめたいやつ
    /// </summary>
    /// <param name = "collision" ></ param >
    private void OnCollisionExit2D(Collision2D collision)
    {
        _jumpChecker = 0;
    }

    /// <summary>
    /// 自分の絵を左右反転させる
    /// </summary>
    /// <param name="horizontal"></param>
    void FlipX(float horizontal)
    {
        /*
         * 左を入力されたら[キャラクターを左に向ける。
         * 左右を反転させるには、Transform:Scale:X に -1 を掛ける。
         * Sprite Renderer の Flip:X を操作しても反転する。
         * */
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
}