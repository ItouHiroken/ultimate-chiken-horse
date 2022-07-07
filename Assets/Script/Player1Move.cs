using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1Move : MonoBehaviour
{
    /// <summary>左右移動する力</summary>
    [SerializeField] float _speed = 5f;
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


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
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
        //ボタンを話すと止まる
        else
        {
            // _rb.velocity = Vector2.zero;
        }
        if (_rb.velocity.y < -30.0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, -30.0f);
        }

        //if (_wallCheck)
        //{
        //    if (_rb.velocity.y < -15.0f)
        //    {
        //        _rb.velocity = new Vector2(_rb.velocity.x, -15.0f);
        //    }
        //}
        if (_rb.velocity.y > 30.0f)
        {
            _rb.velocity = new Vector2(_rb.velocity.x, 30.0f);
        }
        if (_rb.velocity.x > 30.0f)
        {
            _rb.velocity = new Vector2(30.0f, _rb.velocity.y);
        }
        if (_rb.velocity.x < -30.0f)
        {
            _rb.velocity = new Vector2(-30.0f, _rb.velocity.y);
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
        //float horizontalKey = Input.GetAxis("Horizontal");
        startPosition = collision.gameObject.transform.position.x;
        myPosition = this.transform.position.x;
        //if (Input.GetKeyDown(KeyCode.Space))
        //{

        //    if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && myPosition > startPosition)
        //    {
        //        Debug.Log("ue");
        //        _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        //        _rb.AddForce(Vector2.right * 10, ForceMode2D.Impulse);

        //    }
        //    if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && myPosition < startPosition)
        //    {
        //        Debug.Log("sita");
        //        _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        //        _rb.AddForce(Vector2.left * 10, ForceMode2D.Impulse);

        //    }
        //}
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
        Debug.Log("i");
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
