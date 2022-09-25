using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;
/// <summary>
/// �v���C���[�̈ړ��Ɋւ������
/// </summary>
public class PlayerMove : MonoBehaviour
{
    [Header("�����̏��")]
    public DeBuff _deBuff = DeBuff.Default;
    public GameManager.Turn Turn;
    [Header("�W�����v")]
    [Tooltip("�W�����v��"), SerializeField] float _jumpPower = 40f; protected float JumpPower { get { return _jumpPower; } }

    protected bool isreturn = false;

    Rigidbody2D _rb = default;
    [Header("����������")]

    [SerializeField] private int _jumpChecker = 0;
    [SerializeField] bool _groundCheck; protected bool GroundCheck { get { return _groundCheck; } }
    [SerializeField] bool _rightWallCheck; protected bool RightWallCheck { get { return _leftWallCheck; } }
    [SerializeField] bool _leftWallCheck; protected bool LeftWallCheck { get { return _rightWallCheck; } }
    [SerializeField][Tooltip("�Ⴄ���C���[�œ����蔻��Ƃ��I")] private LayerMask levelMask;


    [Header("���̓{�^���̖��O")]
    [SerializeField] string _jump;
    [SerializeField] string _horizontal;

    [Header("����������")]
    [SerializeField][Tooltip("�̗�")] private int _hp = default;
    [SerializeField][Tooltip("���E�̑��x")] private float _horizonSpeedLimiter;
    [SerializeField][Tooltip("�㉺�̑��x")] private float _jumpSpeedLimiter;

    [Header("���E�ړ����鑬�x")]
    [Tooltip("���ݑ��x")][SerializeField] private float _speed;

    [Tooltip("�ʏ푬�x")] private float _defaultSpeed = 5f;

    [Tooltip("�X���E���x")] private float _slowSpeed = default;

    [Tooltip("���������x")] private float _splitSpeed = default;

    [Tooltip("���������̑��x����"), SerializeField] private float _walkSpeedLimiter = 30f;

    [Header("�^�[���͔c���p")]
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameManager _gameManager;

    [Header("�|�C���g�֌W")]
    public PlayerState.GetScore Score;
    [SerializeField, Tooltip("�S�[���Ɏ�����n������")] GameObject _goal;
    [SerializeField] public int _scorePoint;

    [Header("���ƃA�j���[�V����")]
    [SerializeField] Animator animator;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClipJump;
    [SerializeField] AudioClip _audioClipDamage;
    [SerializeField] AudioClip _audioClipCoin;

    [Header("����������")]
    [SerializeField][Tooltip("�����̓���onoff���邽��")] PlayerMove controller;

    //[Tooltip("����邩�ǂ����`�F�b�N")] bool _dashCheck;
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
        if (Input.anyKeyDown)
        {
            foreach (KeyCode code in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(code))
                {
                    //����������
                    Debug.Log(code);
                    break;
                }
            }
        }
        ////
        SpeedController();
        TurnChecker();
        if (Turn == GameManager.Turn.GamePlay)
        {
            bool jump = Input.GetButtonDown(_jump);
            if (jump)
            {
                Debug.Log(_horizontal);
                if (_jumpChecker == 1 && GroundCheck)
                {
                    _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                if (_jumpChecker == 1 && !GroundCheck && RightWallCheck)
                {
                    _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
                if (_jumpChecker == 1 && !GroundCheck && LeftWallCheck)
                {
                    _rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                    _rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
                    _audioSource.PlayOneShot(_audioClipJump);
                }
            }
        }
        RightWallCheker(Vector3.right); // �E�ɍL����
        LeftWallCheker(Vector3.left); // ���ɍL����
        GroundCheker(Vector3.down); // ���ɍL����
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
            if (_rb.velocity.y < -_jumpSpeedLimiter)
            {
                _rb.velocity = new Vector2(_rb.velocity.x, -_jumpSpeedLimiter);
            }

            if (RightWallCheck || LeftWallCheck)
            {
                if (_rb.velocity.y < -_jumpSpeedLimiter / 2)
                {
                    _rb.velocity = new Vector2(_rb.velocity.x, -_jumpSpeedLimiter / 2);
                }
            }
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
    /// �_���[�W�󂯂�p
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
        if (collision.gameObject.CompareTag("Coin"))
        {
            _audioSource.PlayOneShot(_audioClipCoin);
            collision.gameObject.tag = "isUsed";
            Score |= PlayerState.GetScore.Coin;
            Debug.Log(Score);
        }
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
    /// �����̊G�����E���]������
    /// </summary>
    /// <param name="horizontal"></param>
    public void FlipX(float horizontal)
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
    void TurnChecker()
    {
        Turn = _gameManager.NowTurn;
    }
}