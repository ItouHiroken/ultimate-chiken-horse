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
    [SerializeField, Tooltip("�I�����ł���{�^����InputManager���̖��O")] string _selectButton;

    [Header("�ϐ�")]
    [Tooltip("�ړ����x")] public float _speed = 10.0f;
    [SerializeField] bool isFollowing;

    [Header("�C���X�^���X��������")]
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;
    [SerializeField] public GameManager.Turn Turn;
    [SerializeField] GameObject OverlapItem;


    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        TurnChecker(_gameManager);
        CursorMove();
        FollowCursol(OverlapItem);
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        OverlapItem = collision.gameObject;
    }

    private void FollowCursol(GameObject gameObject)
    {
        if (gameObject == null) return;
        if (!gameObject.TryGetComponent<ItemKaiten>(out ItemKaiten _Item)) return;
        FollowCursor(gameObject.gameObject, isFollowing);
        switch (Turn)
        {
            case GameManager.Turn.SelectItem:
                if (Input.GetButtonDown(_selectButton))
                {
                    isFollowing = true;
                    _gameManager.GetComponent<GameManager>()._isChoiceCursol.Add(base.gameObject);
                }
                break;
            case GameManager.Turn.SetItem:
                if (Input.GetButtonDown(_selectButton))
                {
                    isFollowing = false;
                    if (!_gameManager.GetComponent<GameManager>()._isPutCursol.Contains(gameObject))
                    {
                        _gameManager.GetComponent<GameManager>()._isPutCursol.Add(base.gameObject);
                    }
                }
                break;
            default:
                break;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        OverlapItem = null;
    }
    /// <summary>
    /// �A�C�e���ɃJ�[�\�������킹�����ƁA��������̑��������ƁA���Ă��Ăق����B
    /// </summary>
    void FollowCursor(GameObject gameObject, bool isFollowing)
    {
        if (isFollowing)
        {
            gameObject.transform.position = this.gameObject.transform.position;
            if (gameObject.TryGetComponent(out DestroyItem destroyItem))
            {
                destroyItem._isSelect = true;
            }
        }
        else
        {
            if (gameObject.TryGetComponent(out Bomb bomb))
            {
                bomb._use = true;
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
            float verticalInput = _speed * Input.GetAxisRaw(_vertical);
            float horizontalInput = _speed * Input.GetAxisRaw(_horizontal);
            rb.velocity = new Vector2(horizontalInput, verticalInput);
        }
    }
}