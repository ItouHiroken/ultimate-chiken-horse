using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���A�C�e���I����ʂɗ������g���J�[�\�� 
/// </summary>
public class Player1Cursor : MonoBehaviour
{
    [Tooltip("�ړ����x")] public float _speed = 10.0f;
    [SerializeField][Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;
    public GameManager.Turn Turn;
    private Rigidbody2D rb;
    [SerializeField] bool isFollowing;

    [SerializeField] GameObject OverlapItem;
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
                if (Input.GetButtonDown("P1Fire"))
                { 
                    isFollowing = true;
                    _gameManager.GetComponent<GameManager>()._isChoiceCursol.Add(base.gameObject);
                    gameObject.GetComponent<SelectCheck>().selected = true;
                }
                break;
            case GameManager.Turn.SetItem:
                if (Input.GetButtonDown("P1Fire")) 
                {
                    isFollowing = false;
                    _gameManager.GetComponent<GameManager>()._isPutCursol.Add(base.gameObject);
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
                destroyItem._isSelect=true;
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
            float verticalInput = _speed * Input.GetAxisRaw("P1Vertical");
            float horizontalInput = _speed * Input.GetAxisRaw("P1Horizontal");
            rb.velocity = new Vector2(horizontalInput, verticalInput);
        }
    }
}