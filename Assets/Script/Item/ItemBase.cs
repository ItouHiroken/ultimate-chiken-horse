using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���������ʂ̋@�\
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
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X)&&p1Follow)
        {
            Debug.Log("X�������ꂽ��");
            _isFollowing = !_isFollowing;///����������������������������悤�ɂ�����
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Cursor"))
        {
            Debug.Log("CursorTriggerEnter");
            _followingCursor = collision.gameObject;
            playername = collision.gameObject.name;
            if (playername == "Player1Cursor")
            {
                p1Follow = true;
                //Activate1();
                if (_isFollowing == true)
                {
                    FollowCursor(collision.gameObject, _isFollowing);
                    ColliderOnOff(true);
                }
                else
                {
                    FollowCursor(gameObject, _isFollowing);
                    //ColliderOnOff(false);
                }
                ChangeColor(true);
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag=="Cursor")
        {
                ChangeColor(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collider)
    {
        if (collider.tag == "Cursor")
        {
            ChangeColor(false);
        }
        p1Follow=false;


       // p2Follow = false;


        //if (playername == "Player2")
        //{
        //    ChangeColor(false);
        //}
    }

    /// <summary>
    /// �A�C�e���I�����A�A�C�e���ɃJ�[�\�����킳��ƐF���ς��B
    /// </summary>
    /// <param name="cursorcheck"></param>
    private void ChangeColor(bool cursorcheck)
    {
        Debug.Log("�F�ς��}��");
        Color color = cursorcheck ? new Color(100,100,100, 255) : new Color(255, 255, 255, 255);
        gameObject.GetComponent<SpriteRenderer>().color = color;
    }

    /// <summary>
    /// �A�C�e���ɃJ�[�\�������킹�����ƁA��������̑��������ƁA���Ă��Ăق����B
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
    /// �I�����ꂽ��R���C�_�[���S���Ȃ��Ȃ��ĂĂق����A�܂��u���Ƃ��̓R���C�_�[�܂��߂��Ă��炤�B
    /// </summary>
    /// <param name="colliderSwitch"></param>
    void ColliderOnOff(bool colliderSwitch)
    {
        GetComponent<BoxCollider2D>().enabled = colliderSwitch;
        //      GetComponent<CircleCollider2D>().enabled = colliderSwitch;
    }
}
