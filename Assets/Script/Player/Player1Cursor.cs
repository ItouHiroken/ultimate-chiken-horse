using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアイテム選択画面に来た時使うカーソル 
/// </summary>
public class Player1Cursor : MonoBehaviour
{
    [Tooltip("移動速度")] public float _speed = 10.0f;
    [SerializeField][Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
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
    /// アイテムにカーソルを合わせたあと、何かしらの操作をすると、ついてきてほしい。
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