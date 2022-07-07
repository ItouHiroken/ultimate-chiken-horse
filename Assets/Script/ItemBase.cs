using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ItemBase : MonoBehaviour
{
    Vector2 _CursorPosition;
    private string playername;
    [SerializeField] int _Hp = 1;
    public abstract void Activate1();
    public abstract void Activate2();

    GameObject _followingCursor;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            _followingCursor = collision.gameObject;
            playername = collision.gameObject.name;
            if (playername == "Player1Cursor")
            {
                Activate1();
                ChangeColor(true);
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    ColliderOnOff(true);
                    FollowCursor();
                }
            }
            //if (playername == "Player2Cursor")
            //{
            //    FollowCursor();
            //    Activate2();
            //    ChangeColor(true);
            //}
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
    }
    private void OnTriggerExit(Collider2D collider)
    {
        if (playername == "Player")
        {
            ChangeColor(false);
        }
        //if (playername == "Player2")
        //{
        //    ChangeColor(false);
        //}
    }
    //public void LostItemIncrease()
    //{
    //    MonoOtosuHito script; //呼ぶスクリプトにあだなつける
    //    GameObject obj = GameObject.Find("Sorakarabusshi"); //Playerっていうオブジェクトを探す
    //    script = obj.GetComponent<MonoOtosuHito>(); //付いているスクリプトを取得
    //    script._lostItem += 1;
    //    Destroy(gameObject);
    //}
    private void ChangeColor(bool cursorcheck)
    {
        Color color = cursorcheck ? new Color(0, 0, 0, 200) : new Color(0, 0, 0, 255);
        GetComponent<Renderer>().material.color = color;
    }
    void FollowCursor()
    {
        _CursorPosition = _followingCursor.transform.position;
        this.transform.position = _CursorPosition;
    }
    void ColliderOnOff(bool colliderSwitch)
    {
        GetComponent<BoxCollider2D>().enabled = colliderSwitch;
        GetComponent<CircleCollider2D>().enabled = colliderSwitch;
    }
}
