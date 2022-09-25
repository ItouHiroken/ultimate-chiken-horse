using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

/// <summary>
/// �A�C�e���j��ł��锚�e
/// </summary>
public class Bomb : ItemBase
{
    [Header("�����������ϐ�����")]
    [Tooltip("�J�[�\���ɂ��ꂩ��g���邼���ċ����Ă��炤")] public bool _use;

    [Header("�A�T�C����������������")]
    [SerializeField] GameObject _manager;
    [SerializeField] GameObject _childObject;

    [Header("���ƃA�j���[�V����")]
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
        base.TurnChecker();
        UseBomb();
    }
    /// <summary>
    /// �����ɐG�ꂽ"�ݒu���ꂽ�A�C�e��"��j�󂷂�
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
    /// �g��ꂽ�玩����istrigger�O���đ��̃A�C�e���ɓ�����悤�ɂ���
    /// </summary>
    void UseBomb()
    {
        if (_use && _nowTurn == GameManager.Turn.SetItem)
        {
            Debug.Log("����ꂽ��");
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