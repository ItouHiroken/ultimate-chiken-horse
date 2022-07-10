using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] private DeBuff _deBuff = DeBuff.Default;
    [SerializeField] private GotScore _gotScore = GotScore.Default;
    /// <summary>左右移動する力</summary>
    [Tooltip("現在速度")][SerializeField] float _speed;
    [Tooltip("通常速度")][SerializeField] float _defaultSpeed;
    [Tooltip("スロウ速度")][SerializeField] float _slowSpeed;
    [Tooltip("滑った速度")][SerializeField] float _splitSpeed;
    [Tooltip("速度制限")][SerializeField] float _speedLimiter;
    /// <summary>ジャンプする力</summary>
    [SerializeField] float _jumpPower = default;
    /// <summary>水平方向の入力値</summary>
    float _horizontal = default;
    /// <summary>入力に応じて左右を反転させるかどうかのフラグ</summary>
    [SerializeField] bool _flipX = false;
    public object AddForce { get; private set; }

    public bool isreturn = false;

    Rigidbody2D _rb = default;

    [SerializeField] private int jumpcheker = 0;

    [SerializeField] bool _groundCheck;
    [SerializeField] bool _wallCheck;

    [SerializeField] float startPosition;
    [SerializeField] float myPosition;

    public bool isDead;
    public bool isGoal1 = false;


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _slowSpeed = _defaultSpeed / 2;
        _splitSpeed = _defaultSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(1))
        {
            Debug.Log(_deBuff);
        }
        _horizontal = Input.GetAxisRaw("Horizontal");
        // 設定に応じて左右を反転させる
        if (_flipX)
        {
            FlipX(_horizontal);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (jumpcheker == 1 && _groundCheck)
            {
                Debug.Log("a");
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }
            if (jumpcheker == 1 && !_groundCheck && _wallCheck && myPosition > startPosition)
            {
                Debug.Log("ue");
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);

            }
            if (jumpcheker == 1 && !_groundCheck && _wallCheck && myPosition < startPosition)
            {
                Debug.Log("shita");
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);

            }
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


        //if (_wallCheck)
        //{
        //    _rb.AddForce(Vector2.down * 0.3f, ForceMode2D.Impulse);
        //}
    }
    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

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
        if (_rb.velocity.y < -_speedLimiter)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -_speedLimiter);
        }

        if (_wallCheck)
        {
            if (_rb.velocity.y < -_speedLimiter/2)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_speedLimiter/2);
            }
        }
        if (_rb.velocity.y > _speedLimiter)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, _speedLimiter);
        }
        if (_rb.velocity.x > _speedLimiter)
        {
            _rb.velocity = new Vector2(_speedLimiter, _rb.velocity.y);
        }
        if (_rb.velocity.x < -_speedLimiter)
        {
            _rb.velocity = new Vector2(-_speedLimiter, _rb.velocity.y);
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
        jumpcheker = 1;
        startPosition = collision.gameObject.transform.position.x;
        myPosition = this.transform.position.x;
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            _groundCheck = false;
        }
        if (collision.gameObject.tag == "Wall")
        {
            _wallCheck = true;
        }
        jumpcheker = 0;
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
}

public enum DeBuff
{
    Default = 1 << 0,
    Slow = 1 << 1,
    Split = 1 << 2
}
public enum GotScore
{
    Default = 1 << 0,
    isGoal = 1 << 1,
    Solo = 1 << 2,
    First = 1 << 3,
    Death = 1 << 4,
    Coin = 1 << 5
}
