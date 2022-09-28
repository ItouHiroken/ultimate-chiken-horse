using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// アイテム破壊できる爆弾
/// </summary>
public class Bomb : ItemBase
{
    [Header("見たいだけ変数くん")]
    [Tooltip("カーソルにこれから使われるぞって教えてもらう")] public bool _use;

    [Header("アサインしたい物を入れる")]
    [SerializeField] GameObject _manager;
    [SerializeField] GameObject _childObject;

    [Header("音とアニメーション")]
    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    CircleCollider2D _circleCollider;
    protected new void Start()
    {
        base.Start();
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    protected new void Update()
    {
        base.Update();
        UseBomb();
    }
    /// <summary>
    /// 自分に触れた"設置されたアイテム"を破壊する
    /// </summary>
    /// <param name="collision"></param>
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("isChoice"))
        {
            Destroy(collision.gameObject);
        }
    }
    IEnumerator SetBomb(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    /// <summary>
    /// 使われたら自分のistrigger外して他のアイテムに当たるようにする
    /// </summary>
    void UseBomb()
    {
        if (_use && _nowTurn == GameManager.Turn.SetItem)
        {
            Debug.Log("つかわれたよ");
            _circleCollider.isTrigger = false;
            _anim.SetBool("Use", true);

            StartCoroutine(SetBomb(2.3f, () =>
            {
                gameObject.transform.position = new Vector3(1000, 1000, 1000);
                _audioSource.PlayOneShot(_audioClip);
            }));
            _use = false;
        }
    }
    protected override void ChangeColor(bool cursorcheck, Color color)
    {
        if (cursorcheck)
        {
            _childObject.gameObject.GetComponent<SpriteRenderer>().color = color;

        }
        else if (!cursorcheck)
        {
            _childObject.gameObject.GetComponent<SpriteRenderer>().color = color;
        }
    }
}