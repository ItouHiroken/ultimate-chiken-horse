using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Player1State;
public abstract class PlayerBase : MonoBehaviour
{
    public DeBuff _deBuff = DeBuff.Default;
    /// <summary>���E�ړ������</summary>
    [Tooltip("���ݑ��x")][SerializeField] private float _speed;
    public float Speed { get { return _speed; } }
    [Tooltip("�ʏ푬�x")] private float _defaultSpeed = 5f;
    public float DefaultSpeed { get { return _defaultSpeed; } }

    [Tooltip("�X���E���x")] private float _slowSpeed = default;
    public float SlowSpeed { get { return _slowSpeed; } }

    [Tooltip("���������x")] private float _splitSpeed = default;
    public float SplitSpeed { get { return _splitSpeed; } }

    [Tooltip("���������̑��x����")][SerializeField] private float _walkSpeedLimiter = 30f;
    public float WalkSpeedLimiter { get { return _walkSpeedLimiter; } }

    [Tooltip("����Ƃ��̑��x����")][SerializeField] private float _runSpeedLimiter = default;
    public float RunSpeedLimiter { get { return _runSpeedLimiter; } }
    /// <summary>�W�����v�����</summary>
    [Tooltip("�W�����v��")][SerializeField] float _jumpPower = 40f;
    public float JumpPower { get { return _jumpPower; } }
    /// <summary>���������̓��͒l</summary>
    float _horizontal = default;
    /// <summary>���͂ɉ����č��E�𔽓]�����邩�ǂ����̃t���O</summary>
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
    [SerializeField][Tooltip("�Ⴄ���C���[�œ����蔻��Ƃ��I")] private LayerMask levelMask;

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
        RightWallCheker(Vector3.right); // �E�ɍL����
        LeftWallCheker(Vector3.left); // ���ɍL����
        GroundCheker(Vector3.down); // ���ɍL����

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
        if (RightWallCheck || LeftWallCheck)
        {
            Rb.AddForce(Vector2.down * 0.3f, ForceMode2D.Impulse);
        }
        if (gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            //�����Ŏ����̎���off�ɂ�����
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        _jumpChecker = 1;
    }

    /// <summary>
    /// �^�O�ŃI���I�t�����Ă�A��߂������
    /// </summary>
    /// <param name = "collision" ></ param >
    private void OnCollisionExit2D(Collision2D collision)
    {
        _jumpChecker = 0;
    }

    /// <summary>
    /// �����̊G�����E���]������
    /// </summary>
    /// <param name="horizontal"></param>
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

    /// <summary>
    /// �E�Ƀ��C��΂��ăE�H�[���`�F�b�N��onoff����A�ł��ĂȂ�
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void RightWallCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // �u���b�N�Ƃ̓����蔻��̌��ʂ��i�[����ϐ�
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // ���E�ɉ������݂��Ȃ��ꍇ
            if (!hit.collider)
            {
                _rightWallCheck = false;
            }
            // ���E�Ƀu���b�N�����݂���ꍇ
            else
            {
                _rightWallCheck = true;
            }
        }
    }

    /// <summary>
    /// ���Ƀ��C��΂��ăE�H�[���`�F�b�N��onoff����A�ł��ĂȂ�
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void LeftWallCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // �u���b�N�Ƃ̓����蔻��̌��ʂ��i�[����ϐ�
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // ���E�ɉ������݂��Ȃ��ꍇ
            if (!hit.collider)
            {
                _leftWallCheck = false;
            }
            // ���E�Ƀu���b�N�����݂���ꍇ
            else
            {
                _leftWallCheck = true;
            }
        }
    }

    /// <summary>
    /// �����Ƀ��C��΂��ăO���E���h�`�F�b�N��onoff����A�ł��ĂȂ�
    /// </summary>
    /// <param name="direction"></param>
    /// <returns></returns>
    private void GroundCheker(Vector3 direction)
    {
        for (int i = 1; i < 2; i++)
        {
            // �u���b�N�Ƃ̓����蔻��̌��ʂ��i�[����ϐ�
            RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 1, levelMask);
            // ���ɉ������݂��Ȃ��ꍇ
            if (!hit.collider)
            {
                _groundCheck = false;
            }
            // ���Ƀu���b�N�����݂���ꍇ
            else
            {
                _groundCheck = true;
            }
        }
    }
}