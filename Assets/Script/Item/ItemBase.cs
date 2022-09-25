using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテムたち共通の機能
/// </summary>
public abstract class ItemBase : MonoBehaviour
{
    Vector2 _CursorPosition;
    private string playername;
    //    public abstract void Activate1();
    //    public abstract void Activate2();
    [SerializeField] protected bool _isFollowing;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    [SerializeField] protected GameManager _gameManager;
    [SerializeField] protected GameManager.Turn _nowTurn;
    [SerializeField] protected GameObject _selectImage;
    protected void Start()
    {
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").gameObject.GetComponent<GameManager>();
    }
    protected void Update()
    {
        if (_nowTurn == GameManager.Turn.SetItem && gameObject.tag != "isChoice")
        {
            Destroy(gameObject);
        }
        if (gameObject.CompareTag("isChoice"))
        {
            ChangeColor(true, _color2);
        }
        ShowImage();
    }
    protected void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            ColliderOnOff(true);
            ChangeColor(true, _color1);
        }
    }
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursor"))
        {
            ChangeColor(true, _color1);
        }
    }
    protected void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Cursor"))
        {
            ChangeColor(false, _color2);
        }
    }

    /// <summary>
    /// 動くアイテムがどのように動くかプレイヤーたちに教える画像があったら画像のOnOff切り替えしてほしい
    /// </summary>
    protected void ShowImage()
    {
        if (_selectImage != null)
        {
            if (_gameManager.NowTurn == GameManager.Turn.SelectItem)
            {
                _selectImage.gameObject.SetActive(true);
            }
            else
            {
                _selectImage.gameObject.SetActive(false);
            }
        }
    }
    /// <summary>
    /// アイテム選択時、アイテムにカーソル合わさると色が変わる。
    /// </summary>
    /// <param name="cursorcheck"></param>
    protected virtual void ChangeColor(bool cursorcheck, Color color)
    {

        if (cursorcheck)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;

        }
        else if (!cursorcheck)
        {
            gameObject.GetComponent<SpriteRenderer>().color = color;
        }

    }
    /// <summary>
    /// 選択されたらコライダーが全部なくなっててほしい、また置くときはコライダーまた戻ってもらう。
    /// </summary>
    /// <param name="colliderSwitch"></param>
    void ColliderOnOff(bool colliderSwitch)
    {
        if (TryGetComponent(out BoxCollider2D BC2D))
        {
            BC2D.enabled = colliderSwitch;
        }
        if (TryGetComponent(out CircleCollider2D CC2D))
        {
            CC2D.enabled = colliderSwitch;
        }
        if (TryGetComponent(out PolygonCollider2D PC2D))
        {
            PC2D.enabled = colliderSwitch;
        }
    }
    protected void TurnChecker()
    {
        _nowTurn = _gameManager.NowTurn;
    }
}
