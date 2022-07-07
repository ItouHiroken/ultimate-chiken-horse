using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player1 : MonoBehaviour
{   /// <summary>�̗�</summary>
    [SerializeField] float _hp = 1f;
    /// <summary>���E�ړ������</summary>
    [SerializeField] float _speed = 5f;
    /// <summary>�W�����v�����</summary>
    [SerializeField] float _jumpPower = 15f;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
    [SerializeField] bool _flipX = false;
    Rigidbody2D _rb = default;
    /// <summary>���������̓��͒l</summary>
    public int jumpcheker = 0;
    float m_h;
    /// <summary>�ŏ��ɏo���������W</summary>
    Vector3 m_initialPosition;

    //public int C = 0;
    public object AddForce { get; private set; }

    public bool isreturn = false;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        // �����ʒu���o���Ă���
        m_initialPosition = this.transform.position;
    }

    void Update()
    {
        Debug.Log(GetComponent<Rigidbody2D>().velocity);
        // ���͂��󂯎��
        m_h = Input.GetAxisRaw("Horizontal");

        // �e����͂��󂯎��
        if (Input.GetButtonDown("Jump"))
        {

            // Debug.Log("�����ɃW�����v���鏈���������B");
            // m_rb.AddForce(Vector2.up*30* m_jumpPower);

            //else if (jimen == 2)
            //{
            //    _rb.AddForce(new Vector3(0, m_jumpPower * 40, 0));
            //    jimen = 0;
            //}
            //  m_rb.AddForce(Vector2.right * m_h * m_movePower, ForceMode2D.Force);
        }
        // ���ɍs���������珉���ʒu�ɖ߂�
        if (this.transform.position.y < -10f)
        {
            this.gameObject.SetActive(false);
        }

        // �ݒ�ɉ����č��E�𔽓]������
        if (_flipX)
        {   
            FlipX(m_h);
        }
    }

    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        //�E���͂ō������ɓ���
        if (horizontalKey > 0)
        {
            _rb.AddForce(new Vector2(_speed * 40, 0));
        }
        //�����͂ō������ɓ���
        else if (horizontalKey < 0)
        {
            _rb.AddForce(new Vector2(-_speed * 40, 0));
        }
        //�{�^����b���Ǝ~�܂�
        else
        {
            _rb.velocity = Vector2.zero;
        }
    }

    /// <summary>
    /// ���E�𔽓]������
    /// </summary>
    /// <param name="horizontal">���������̓��͒l</param>
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
                _rb.AddForce(Vector2.left *100, ForceMode2D.Impulse);

            }
            if (jumpcheker == 1 && collision.gameObject.tag != "Ground" && collision.gameObject.tag == "Wall" && horizontalKey > 0)
            {
                Debug.Log("sita");
                _rb.AddForce(new Vector2(0, _jumpPower * 40));
                _rb.AddForce(Vector2.right *100, ForceMode2D.Impulse);

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
