using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player1State;
public abstract class PlayerBase : MonoBehaviour
{
    [Tooltip("現在速度")][SerializeField] private int _playerHp;
    public int Hp { get { return _playerHp; } }

    [SerializeField] private DeBuff _deBuff = DeBuff.Default;
    [SerializeField] private GetScore _getScore = GetScore.Default;
    /// <summary>左右移動する力</summary>
    [Tooltip("現在速度")][SerializeField] private float _speed;
    public float Speed { get { return _speed; } }
    [Tooltip("通常速度")] private float _defaultSpeed = 5f;
    public float DefaultSpeed { get { return _defaultSpeed; } }

    [Tooltip("スロウ速度")] private float _slowSpeed = default;
    public float SlowSpeed { get { return _slowSpeed; } }

    [Tooltip("滑った速度")] private float _splitSpeed = default;
    public float SplitSpeed { get { return _splitSpeed; } }

    [Tooltip("速度制限")][SerializeField] private float _walkSpeedLimiter = 30f;
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
    [SerializeField] bool _wallCheck;
    public bool WallCheck { get { return _wallCheck; } }

    [SerializeField] float _startPosition;
    public float StartPosition { get { return _startPosition; } }

    [SerializeField] float _myPosition;
    public float MyPosition { get { return _myPosition; } }

    public bool isDead;
    public bool isGoal1 = false;
    [SerializeField][Tooltip("違うレイヤーで当たり判定とるよ！")] private LayerMask levelMask;

    // Start is called before the first frame update
    void Start()
    {
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
    void Update()
    {
        StartCoroutine(WallCheker(Vector3.right)); // 右に広げる
        StartCoroutine(WallCheker(Vector3.left)); // 左に広げる
        StartCoroutine(GroundCheker(Vector3.down)); // 下に広げる

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
        if (WallCheck)
        {
            Rb.AddForce(Vector2.down * 0.3f, ForceMode2D.Impulse);
        }

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundCheck = true;
        }
        if (collision.gameObject.tag == "Wall")
        {
            _wallCheck = true;
        }
    }
    void OnCollisionStay2D(Collision2D collision)
    {
        _jumpChecker = 1;
        _startPosition = collision.gameObject.transform.position.x;
        _myPosition = this.transform.position.x;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundCheck = false;
        }
        if (collision.gameObject.tag == "Wall")
        {
            _wallCheck = false;
        }
        _jumpChecker = 0;
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageController damage))
        {
            _playerHp -= damage.Damage;
            if (_playerHp <= 0)
            {

            }
        }
    }
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
    private IEnumerator WallCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit2D hit = Physics2D.Raycast(transform.position , direction, 1, levelMask);
            // 左右に何も存在しない場合
            if (!hit.collider)
            {
                _wallCheck = false;
            }
            // 左右にブロックが存在する場合
            else
            {
                _wallCheck = true;
            }

            yield return new WaitForSeconds(0);
        }
    }
    private IEnumerator GroundCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // ブロックとの当たり判定の結果を格納する変数
            RaycastHit2D hit = Physics2D.Raycast(transform.position  , direction, 1, levelMask);
            Debug.DrawRay(transform.position + new Vector3(0, 0.5f, 0) + direction * i, direction);
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
        yield return new WaitForSeconds(0);
    }
}