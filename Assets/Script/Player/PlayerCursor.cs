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
    [SerializeField, Tooltip("カメラの上のライン")] float _under;
    [SerializeField, Tooltip("カメラの上のライン")] float _top;
    [SerializeField, Tooltip("カメラの上のライン")] float _left;
    [SerializeField, Tooltip("カメラの上のライン")] float _right;

    [Header("見たいだけ")]
    [SerializeField] public bool _isFollowing;
    [SerializeField] GameObject _overlapItem;

    [Header("インスタンスしたい物")]
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameManager _gameManager;
    [SerializeField] GameManager.Turn Turn;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _choiceSound;


    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _gameManager.TurnChangeActionMethod += TurnChecker;
    }
    private void Update()
    {
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
            Debug.Log("拾うまん");
            _overlapItem = collision.gameObject;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _overlapItem = null;
    }
    private void CursolAndItem(GameObject gameObject)
    {
        //ゲームオブジェクトなかったら判定しない
        if (gameObject == null) return;
        //そのゲームオブジェクトにスクリプトがどっちもなかったら判定しない
        if (!(!gameObject.TryGetComponent<ItemKaiten>(out ItemKaiten _Item) || !gameObject.TryGetComponent<FlipX>(out FlipX flipXCheck))) return;
        //ターンによって判定が違う
        switch (Turn)
        {
            case GameManager.Turn.SelectItem:

                //選択ボタン押したら
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.IsChoiceCursol.Contains(this.gameObject))
                {
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = true;//アイテムがついてくるようになる
                    gameObject.tag = "isChoice";//アイテムを選んだ印つけ
                    gameObject.SetActive(false);//アイテムは一回消えてほしい
                    _gameManager.GetComponent<GameManager>().ChoiceList.Add(gameObject);
                    _gameManager.GetComponent<GameManager>().IsChoiceCursol.Add(this.gameObject);//自分は選んだって伝える
                    this.gameObject.SetActive(false);//自分が一回いなくなる
                }
                break;
            case GameManager.Turn.SetItem:
                //選択ボタン押したら
                if (Input.GetButtonDown(_selectButton) &&
                    !_gameManager.IsPutCursol.Contains(this.gameObject))
                {
                    _audioSource.PlayOneShot(_choiceSound);
                    _isFollowing = false;//アイテムが付いてこなくなる

                    //爆弾かどうか判定し、爆弾だった場合使う
                    if (gameObject.TryGetComponent(out Bomb bomb))
                    {
                        bomb._use = true;
                    }
                    _gameManager.IsPutCursol.Add(this.gameObject);//自分が置いたよって伝える
                    Debug.Log(_gameManager.IsPutCursol);
                    this.gameObject.SetActive(false);//自分がいなくなる
                }

                //回転のこと書いてる
                if (Input.GetButtonDown(_kaitenLeftName))
                {
                    Debug.Log("左呼ばれたよ");
                    if (gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.forward);// 現在の自信の回転の情報を取得する。
                        Quaternion q = gameObject.transform.rotation;// 合成して、自身に設定
                        gameObject.transform.rotation = q * rot;
                    }
                    if (gameObject.TryGetComponent(out FlipX flipX))
                    {
                        flipX._flipX = true;
                    }
                }
                if (Input.GetButtonDown(_kaitenRightName))
                {
                    Debug.Log("右呼ばれたよ");
                    if (gameObject.TryGetComponent(out ItemKaiten kaiten))
                    {
                        Quaternion rot = Quaternion.AngleAxis(kaiten._kaitenIndex, Vector3.back);// 現在の自信の回転の情報を取得する。
                        Quaternion q = gameObject.transform.rotation;// 合成して、自身に設定
                        gameObject.transform.rotation = q * rot;
                    }
                    if (gameObject.TryGetComponent(out FlipX flipX))
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
    /// アイテムにカーソルを合わせたあと、何かしらの操作をすると、ついてきてほしい。
    /// </summary>
    void ItemFollowCursor(GameObject gameObject, bool isFollowing)
    {
        if (isFollowing && gameObject != null)
        {
            gameObject.transform.position = this.gameObject.transform.position;
        }
    }
    void TurnChecker()
    {
        Turn = _gameManager.NowTurn;
    }
    void CursorMove()
    {
        if (Turn == GameManager.Turn.SetItem || Turn == GameManager.Turn.SelectItem)
        {
            Vector2 thisPos = this.gameObject.transform.position;
            ///無理やりここから画面外に出るなってした
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