using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアイテム選択画面に来た時使うカーソル 
/// </summary>
public class PlayerCursor : MonoBehaviour
{

    [Header("ジョイスティックの入力 InputManager内の名前")]
    [SerializeField, Tooltip("横")] string _horizontal;
    [SerializeField, Tooltip("縦")] string _vertical;
    [SerializeField, Tooltip("左に回転")] string _kaitenLeftName;
    [SerializeField, Tooltip("右に回転")] string _kaitenRightName;
    [SerializeField, Tooltip("選択ができるボタンのInputManager内の名前")] string _selectButton;

    [Header("変数")]
    [Tooltip("移動速度")] float _speed = 10.0f;

    [Header("見たいだけ")]
    [SerializeField] public bool _isFollowing;
    [SerializeField] GameObject _overlapItem;

    [Header("インスタンスしたい物")]
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
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
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (_overlapItem == null && Turn == GameManager.Turn.SetItem)
        {
            _overlapItem = collision.gameObject;
        }
        if (_overlapItem == null && !collision.CompareTag("isChoice") && Turn == GameManager.Turn.SelectItem)
        {
            Debug.Log("拾うまん");
            _overlapItem = collision.gameObject;
        }
    }

    private void CursolAndItem(GameObject gameObject)
    {
        if (gameObject == null) return;
        if (!gameObject.TryGetComponent<ItemKaiten>(out ItemKaiten _Item)) return;
        ItemFollowCursor(gameObject.gameObject, _isFollowing);
        switch (Turn)
        {
            case GameManager.Turn.SelectItem:

                //選択ボタン押したら
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.GetComponent<GameManager>()._isChoiceCursol.Contains(this.gameObject))
                {
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = true;
                    gameObject.tag = "isChoice";
                    _gameManager.GetComponent<GameManager>()._choiceList.Add(gameObject);
                    gameObject.SetActive(false);
                    _gameManager.GetComponent<GameManager>()._isChoiceCursol.Add(base.gameObject);
                    Debug.Log(_gameManager.GetComponent<GameManager>()._isChoiceCursol);

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
                    Debug.Log("左呼ばれたよ");
                    if (gameObject.gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.forward);
                        // 現在の自信の回転の情報を取得する。
                        Quaternion q = gameObject.transform.rotation;
                        // 合成して、自身に設定
                        gameObject.transform.rotation = q * rot;
                    }
                    if (gameObject.gameObject.TryGetComponent(out FlipX flipX))
                    {
                        flipX._flipX = true;
                    }
                }
                if (Input.GetButtonDown(_kaitenRightName))
                {
                    Debug.Log("右呼ばれたよ");
                    if (gameObject.gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.back);
                        // 現在の自信の回転の情報を取得する。
                        Quaternion q = gameObject.transform.rotation;
                        // 合成して、自身に設定
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
    /// アイテムにカーソルを合わせたあと、何かしらの操作をすると、ついてきてほしい。
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
        else
        {

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