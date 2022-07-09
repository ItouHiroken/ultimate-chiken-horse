using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] private DeBuff _deBuff = DeBuff.Default;
    [SerializeField] private GotScore _gotScore = GotScore.Default;
    /// <summary>���E�ړ������</summary>
    [SerializeField] float _speed = 5f;
    /// <summary>�W�����v�����</summary>
    [SerializeField] float _jumpPower = default;
    /// <summary>���������̓��͒l</summary>
    float _horizontal = default;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
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

    }

    // Update is called once per frame
    void Update()
    {
         Vector2 dir = new Vector2(_horizontal,0).normalized;
        dir.y = _rb.velocity.y;
        _horizontal = Input.GetAxisRaw("Horizontal");
        // �ݒ�ɉ����č��E�𔽓]������
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
        if (_deBuff == DeBuff.Slow)
        {
            _rb.velocity = dir *_speed / 2;
         //   _rb.velocity = dir * _jumpPower / 2;
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

        //�E���͂ō������ɓ���
        if (horizontalKey > 0)
        {
            _rb.AddForce(Vector2.right * _speed, ForceMode2D.Impulse);
        }
        //�����͂ō������ɓ���
        else if (horizontalKey < 0)
        {
            _rb.AddForce(Vector2.left * _speed, ForceMode2D.Impulse);
        }
        //�{�^����b���Ǝ~�܂�
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
         * ������͂��ꂽ��[�L�����N�^�[�����Ɍ�����B
         * ���E�𔽓]������ɂ́ATransform:Scale:X �� -1 ���|����B
         * Sprite Renderer �� Flip:X �𑀍삵�Ă����]����B
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
