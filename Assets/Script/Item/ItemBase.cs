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
    [SerializeField] int _Hp = 1;
    //    public abstract void Activate1();
    //    public abstract void Activate2();

    GameObject _followingCursor;
    [SerializeField] bool _isFollowing;
    bool p1Follow;
    [SerializeField] Color _color1;
    [SerializeField] Color _color2;
    [SerializeField] GameManager _gameManager;

    [SerializeField] GameObject _selectImage;
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


    //if (collision.gameObject.TryGetComponent(out BombBlast bomb))
    //{
    //    _Hp -= bomb._bombDamage;
    //    if (_Hp <= 0)
    //    {
    //        LostItemIncrease();
    //        Destroy(gameObject);
    //    }
    //}
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
            if (collider.name == "P1Cursor")
            {
                p1Follow = false;
            }
        }
        // p2Follow = false;


        //if (playername == "Player2")
        //{
        //    ChangeColor(false);
        //}
    }

    /// <summary>
    /// アイテム選択時、アイテムにカーソル合わさると色が変わる。
    /// </summary>
    /// <param name="cursorcheck"></param>
    private void ChangeColor(bool cursorcheck, Color color)
    {
        Debug.Log("色変わるマン");
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
    /// アイテムにカーソルを合わせたあと、何かしらの操作をすると、ついてきてほしい。
    /// </summary>
    void FollowCursor(GameObject gameObject, bool isFollowing)
    {
        if (isFollowing)
        {
            _CursorPosition = _followingCursor.transform.position;
            this.transform.position = _CursorPosition;
        }
    }

    /// <summary>
    /// 選択されたらコライダーが全部なくなっててほしい、また置くときはコライダーまた戻ってもらう。
    /// </summary>
    /// <param name="colliderSwitch"></param>
    void ColliderOnOff(bool colliderSwitch)
    {
        GetComponent<BoxCollider2D>().enabled = colliderSwitch;
        //      GetComponent<CircleCollider2D>().enabled = colliderSwitch;
    }
}
