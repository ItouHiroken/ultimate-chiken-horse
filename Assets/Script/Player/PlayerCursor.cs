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

    private void CursolAndItem(GameObject gameObject)
    {
        if (gameObject == null)return;
        if (!(!gameObject.TryGetComponent<ItemKaiten>(out ItemKaiten _Item) || !gameObject.TryGetComponent<FlipX>(out FlipX flipXCheck)))return;
        switch (Turn)
        {
            case GameManager.Turn.SelectItem:

                //�I���{�^����������
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.GetComponent<GameManager>()._isChoiceCursol.Contains(this.gameObject))
                {
                    Debug.Log(_gameManager.GetComponent<GameManager>()._isChoiceCursol);
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = true;
                    gameObject.tag = "isChoice";
                    _gameManager.GetComponent<GameManager>()._choiceList.Add(gameObject);
                    gameObject.SetActive(false);
                    _gameManager.GetComponent<GameManager>()._isChoiceCursol.Add(base.gameObject);

                    this.gameObject.SetActive(false);
                }
               
                break;
            case GameManager.Turn.SetItem:
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.GetComponent<GameManager>()._isPutCursol.Contains(this.gameObject))
                {
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = false;
                    if (gameObject.TryGetComponent(out Bomb bomb))
                    {
                        bomb._use = true;
                    }
                    _gameManager.GetComponent<GameManager>()._isPutCursol.Add(base.gameObject);
                    Debug.Log(_gameManager.GetComponent<GameManager>()._isPutCursol);
                    this.gameObject.SetActive(false);
                }
                if (Input.GetButtonDown(_kaitenLeftName))
                {
                    Debug.Log("���Ă΂ꂽ��");
                    if (gameObject.gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.forward);
                        // ���݂̎��M�̉�]�̏����擾����B
                        Quaternion q = gameObject.transform.rotation;
                        // �������āA���g�ɐݒ�
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
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.back);
                        // ���݂̎��M�̉�]�̏����擾����B
                        Quaternion q = gameObject.transform.rotation;
                        // �������āA���g�ɐݒ�
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

    private void OnTriggerExit2D(Collider2D collision)
    {

        _overlapItem = null;
    }
    /// <summary>
    /// �A�C�e���ɃJ�[�\�������킹�����ƁA��������̑��������ƁA���Ă��Ăق����B
    /// </summary>
    void ItemFollowCursor(GameObject gameObject, bool isFollowing)
    {
        if (isFollowing)
        {
            gameObject.transform.position = this.gameObject.transform.position;
            if (gameObject.TryGetComponent(out DestroyItem destroyItem))
            {
                destroyItem._isSelect = true;
            }
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
            float verticalInput = -_speed * Input.GetAxisRaw(_vertical);
            float horizontalInput = _speed * Input.GetAxisRaw(_horizontal);
            rb.velocity = new Vector2(horizontalInput, verticalInput);
        }
    }
}