using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���A�C�e���I����ʂɗ������g���J�[�\�� 
/// </summary>
public class PlayerCursor : MonoBehaviour
{
    [Header("�W���C�X�e�B�b�N�̓��� InputManager���̖��O")]
    [SerializeField, Tooltip("��")] string _horizontal;
    [SerializeField, Tooltip("�c")] string _vertical;
    [SerializeField, Tooltip("���ɉ�]")] string _kaitenLeftName;
    [SerializeField, Tooltip("�E�ɉ�]")] string _kaitenRightName;
    [SerializeField, Tooltip("�I�����ł���{�^����InputManager���̖��O")] string _selectButton;

    [Header("�ϐ�")]
    [Tooltip("�ړ����x")] float _speed = 10.0f;
    [SerializeField, Tooltip("�J�����̏�̃��C��")] float _under;
    [SerializeField, Tooltip("�J�����̏�̃��C��")] float _top;
    [SerializeField, Tooltip("�J�����̏�̃��C��")] float _left;
    [SerializeField, Tooltip("�J�����̏�̃��C��")] float _right;

    [Header("����������")]
    [SerializeField] public bool _isFollowing;
    [SerializeField] GameObject _overlapItem;

    [Header("�C���X�^���X��������")]
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;
    [SerializeField] GameManager.Turn Turn;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _choiceSound;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        TurnChecker(_gameManager);
        CursorMove();
        CursolAndItem(_overlapItem);
        ItemFollowCursor(_overlapItem, _isFollowing);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (_overlapItem == null && Turn == GameManager.Turn.SetItem)
        {
            _overlapItem = collision.gameObject;
        }
        if (_overlapItem == null && !collision.CompareTag("isChoice") && Turn == GameManager.Turn.SelectItem)
        {
            Debug.Log("�E���܂�");
            _overlapItem = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {

        _overlapItem = null;
    }
    private void CursolAndItem(GameObject gameObject)
    {
        //�Q�[���I�u�W�F�N�g�Ȃ������画�肵�Ȃ�
        if (gameObject == null) return;
        //���̃Q�[���I�u�W�F�N�g�ɃX�N���v�g���ǂ������Ȃ������画�肵�Ȃ�
        if (!(!gameObject.TryGetComponent<ItemKaiten>(out ItemKaiten _Item) || !gameObject.TryGetComponent<FlipX>(out FlipX flipXCheck))) return;
        //�^�[���ɂ���Ĕ��肪�Ⴄ
        switch (Turn)
        {
            case GameManager.Turn.SelectItem:

                //�I���{�^����������
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.GetComponent<GameManager>().IsChoiceCursol.Contains(this.gameObject))
                {
                    Debug.Log(_gameManager.GetComponent<GameManager>().IsChoiceCursol);
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = true;//�A�C�e�������Ă���悤�ɂȂ�
                    gameObject.tag = "isChoice";//�A�C�e����I�񂾈��
                    gameObject.SetActive(false);//�A�C�e���͈������Ăق���
                    _gameManager.GetComponent<GameManager>().ChoiceList.Add(gameObject);
                    _gameManager.GetComponent<GameManager>().IsChoiceCursol.Add(this.gameObject);//�����͑I�񂾂��ē`����
                    this.gameObject.SetActive(false);//��������񂢂Ȃ��Ȃ�
                }
                break;
            case GameManager.Turn.SetItem:
                //�I���{�^����������
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.GetComponent<GameManager>().IsPutCursol.Contains(this.gameObject))
                {
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = false;//�A�C�e�����t���Ă��Ȃ��Ȃ�

                    //���e���ǂ������肵�A���e�������ꍇ�g��
                    if (gameObject.TryGetComponent(out Bomb bomb))
                    {
                        bomb._use = true;
                    }
                    _gameManager.GetComponent<GameManager>().IsPutCursol.Add(this.gameObject);//�������u��������ē`����
                    Debug.Log(_gameManager.GetComponent<GameManager>().IsPutCursol);
                    this.gameObject.SetActive(false);//���������Ȃ��Ȃ�
                }

                //��]�̂��Ə����Ă�
                if (Input.GetButtonDown(_kaitenLeftName))
                {
                    Debug.Log("���Ă΂ꂽ��");
                    if (gameObject.gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.forward);// ���݂̎��M�̉�]�̏����擾����B
                        Quaternion q = gameObject.transform.rotation;// �������āA���g�ɐݒ�
                        gameObject.transform.rotation = q * rot;
                    }
                    if (gameObject.gameObject.TryGetComponent(out FlipX flipX))
                    {
                        flipX._flipX = true;
                    }
                }
                if (Input.GetButtonDown(_kaitenRightName))
                {
                    Debug.Log("�E�Ă΂ꂽ��");
                    if (gameObject.gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.back);// ���݂̎��M�̉�]�̏����擾����B
                        Quaternion q = gameObject.transform.rotation;// �������āA���g�ɐݒ�
                        gameObject.transform.rotation = q * rot;
                    }
                    if (gameObject.gameObject.TryGetComponent(out FlipX flipX))
                    {
                        flipX._flipX = true;
                    }
                }
                break;
            default:
                break;
        }
    }


    /// <summary>
    /// �A�C�e���ɃJ�[�\�������킹�����ƁA��������̑��������ƁA���Ă��Ăق����B
    /// </summary>
    void ItemFollowCursor(GameObject gameObject, bool isFollowing)
    {
        if (isFollowing && gameObject != null)
        {
            gameObject.transform.position = this.gameObject.transform.position;
        }
    }
    void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
    void CursorMove()
    {
        if (Turn == GameManager.Turn.SetItem || Turn == GameManager.Turn.SelectItem)
        {
            Vector2 thisPos = this.gameObject.transform.position;
            ///������肱�������ʊO�ɏo��Ȃ��Ă���
            if (_under <= thisPos.y && thisPos.y <= _top && _left <= thisPos.x && thisPos.x <= _right)
            {
                float verticalInput = -_speed * Input.GetAxisRaw(_vertical);
                float horizontalInput = _speed * Input.GetAxisRaw(_horizontal);
                rb.velocity = new Vector2(horizontalInput, verticalInput);
            }
            else if (_under >= thisPos.y)
            {
                rb.velocity = new Vector2(0, 20);
            }
            else if (thisPos.y >= _top)
            {
                rb.velocity = new Vector2(0, -20);
            }
            else if (_left >= thisPos.x)
            {
                rb.velocity = new Vector2(20, 0);
            }
            else if (thisPos.x >= _right)
            {
                rb.velocity = new Vector2(-20, 0);
            }
        }
    }
}