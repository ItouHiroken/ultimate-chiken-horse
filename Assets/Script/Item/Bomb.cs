using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public class Bomb : ItemBase
{
    [Header("å©ÇΩÇ¢ÇæÇØïœêîÇ≠ÇÒ")]
    public bool _use;

    [Header("ÉAÉTÉCÉìÇµÇΩÇ¢ï®Çì¸ÇÍÇÈ")]
    [SerializeField] GameObject _manager;
    [SerializeField] GameObject _childObject;
    [SerializeField] Animator _anim;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    GameManager.Turn _turn;
    CircleCollider2D _circleCollider;
    private void Start()
    {
        _manager = GameObject.Find("GameManager").gameObject;
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    protected new void Update()
    {
        TurnChecker(_manager);
        UseBomb();
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("isChoice"))
        {
            collision.gameObject.transform.position = new Vector3(900, 1000, 1000);

        }
    }
    IEnumerator SetBomb(float seconds, UnityAction callback)
    {
        yield return new WaitForSeconds(seconds);
        callback?.Invoke();
    }

    void UseBomb()
    {
        if (_use && _turn == GameManager.Turn.SetItem)
        {
            Debug.Log("Ç¬Ç©ÇÌÇÍÇΩÇÊ");
            _circleCollider.isTrigger = false;
            _anim.SetBool("Use", true);

            StartCoroutine(SetBomb(2.3f, () => 
            { gameObject.transform.position = new Vector3(1000, 1000, 1000);
                _audioSource.PlayOneShot(_audioClip);}));
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
    void TurnChecker(GameObject a)
    {
        _turn = a.GetComponent<GameManager>().NowTurn;
    }
}