using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{   /// <summary>体力</summary>
    [SerializeField] float _hp = 1f;
    /// <summary>左右移動する力</summary>
    [SerializeField] float _speed = 5f;
    /// <summary>ジャンプする力</summary>
    [SerializeField] float _jumpPower = 15f;
    /// <summary>入力に応じて左右を反転させるかどうかのフラグ</summary>
    [SerializeField] bool _flipX = false;
    Rigidbody2D _rb = default;
    /// <summary>水平方向の入力値</summary>
    public int jumpcheker = 0;
    float m_h;
    /// <summary>最初に出現した座標</summary>
    Vector3 m_initialPosition;

    //public int C = 0;
    public object AddForce { get; private set; }

    public bool isreturn = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // 初期位置を覚えておく
        m_initialPosition = this.transform.position;
    }

    void Update()
    {
        Debug.Log(GetComponent<Rigidbody2D>().velocity);
        // 入力を受け取る
        m_h = Input.GetAxisRaw("Horizontal");

        // 各種入力を受け取る
        if (Input.GetButtonDown("Jump"))
        {

            // Debug.Log("ここにジャンプする処理を書く。");
            // m_rb.AddForce(Vector2.up*30* m_jumpPower);

            //else if (jimen == 2)
            //{
            //    _rb.AddForce(new Vector3(0, m_jumpPower * 40, 0));
            //    jimen = 0;
            //}
            //  m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
        }
        // 下に行きすぎたら初期位置に戻す
        if (this.transform.position.y < -10f)
        {
            this.gameObject.SetActive(false);
        }

        // 設定に応じて左右を反転させる
        if (_flipX)
        {
            FlipX(m_h);
        }
    }

    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        //右入力で左向きに動く
        if (horizontalKey > 0)
        {
            _rb.AddForce(new Vector2(_speed * 40, 0));
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            _rb.AddForce(new Vector2(-_speed * 40, 0));
        }
        //ボタンを話すと止まる
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// 左右を反転させる
    /// </summary>
    /// <param name="horizontal">水平方向の入力値</param>
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
    void OnCollisionEnter2D(Collision2D collision)
    {
        jumpcheker = 1;
        float horizontalKey = Input.GetAxis("Horizontal");
        if (Input.GetButtonDown("Jump"))
        {
            if (jumpcheker == 1 && collision.gameObject.tag == "Ground")
            {
                _rb.AddForce(new Vector2(0, _jumpPower * 40));
            }
            if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && horizontalKey < 0)
            {
                Debug.Log("ue");
                _rb.AddForce(new Vector2(0, _jumpPower * 40));
                _rb.AddForce(Vector2.left * 100, ForceMode2D.Impulse);

            }
            if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && horizontalKey > 0)
            {
                Debug.Log("sita");
                _rb.AddForce(new Vector2(0, _jumpPower * 40));
                _rb.AddForce(Vector2.right * 100, ForceMode2D.Impulse);

            }
        }
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        //jumpcheker = 1;
        //float horizontalKey = Input.GetAxis("Horizontal");
        //if (Input.GetButtonDown("Jump"))
        //{
        //    if (jumpcheker == 1 && collision.gameObject.tag == "Ground")
        //    {
        //        _rb.AddForce(new Vector2(0, _jumpPower * 40));
        //    }
        //    if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && horizontalKey < 0)
        //    {
        //        Debug.Log("ue");
        //        _rb.AddForce(new Vector2(100, _jumpPower * 40));
        //    }
        //    if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && horizontalKey > 0)
        //    {
        //        Debug.Log("sita");
        //        _rb.AddForce(new Vector2(-100, _jumpPower * 40));
        //    }
        //}
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        jumpcheker = 0;
    }
}
