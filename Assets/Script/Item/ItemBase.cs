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

    [SerializeField] bool _isFollowing;
    bool p1Follow;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    [SerializeField] protected GameManager _gameManager;

    [SerializeField] GameObject _selectImage;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }
    private void Update()
    {
        if (_selectImage == null) return;
        if (_gameManager.NowTurn == GameManager.Turn.SelectItem)
        {
            _selectImage.gameObject.SetActive(true);
        }
        else
        {
            _selectImage.gameObject.SetActive(false);
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            ColliderOnOff(true);
            ChangeColor(true, _color1);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Cursor"))
        {
            ChangeColor(true, _color1);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.CompareTag("Cursor"))
        {
            ChangeColor(false, _color2);
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
    }
}
