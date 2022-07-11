using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �v���C���[�̃x�[�X
/// </summary>
public abstract class PlayerBase : MonoBehaviour
{
    [SerializeField] private DeBuff _deBuff = DeBuff.Default;
    [SerializeField] private GotScore _gotScore = GotScore.Default;
    /// <summary>���E�ړ������</summary>
    [Tooltip("���ݑ��x")][SerializeField] private float _speed;
    public float Speed { get { return _speed; } }
    [Tooltip("�ʏ푬�x")] private float _defaultSpeed = 5f;
    public float DefaultSpeed { get { return _defaultSpeed; } }

    [Tooltip("�X���E���x")] private float _slowSpeed = default;
    public float SlowSpeed { get { return _slowSpeed; } }

    [Tooltip("���������x")] private float _splitSpeed = default;
    public float SplitSpeed { get { return _splitSpeed; } }

    [Tooltip("���x����")][SerializeField] private float _speedLimiter = 30f;
    public float SpeedLimiter { get { return _speedLimiter; } }
    /// <summary>�W�����v�����</summary>
    [Tooltip("�W�����v��")][SerializeField] float _jumpPower = 40f;
    public float JumpPower { get { return _jumpPower; } }
    /// <summary>���������̓��͒l</summary>
    float _horizontal = default;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
    [SerializeField] bool _flipX = false;
    public object AddForce { get; private set; }

    public bool isreturn = false;

    Rigidbody2D _rb = default;
    public Rigidbody2D Rb { get { return _rb; } }

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


    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _speed = _defaultSpeed;
        _slowSpeed = _defaultSpeed / 2;
        _splitSpeed = _defaultSpeed * 2;
    }

    // Update is called once per frame
    void Update()
    {
        _horizontal = Input.GetAxisRaw("Horizontal");
        // �ݒ�ɉ����č��E�𔽓]������
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
            _wallCheck = true;
        }
        _jumpChecker = 0;
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
