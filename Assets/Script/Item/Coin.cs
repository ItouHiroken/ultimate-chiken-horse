using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : ItemBase
{
    [SerializeField] GameManager.Turn nowTurn;
    GameObject _collisionPlayer;
    bool _isCollision = false;
    bool _flag;
    float time;
    private void Update()
    {
        TurnChecker();
        if (nowTurn == GameManager.Turn.GamePlay && _isCollision)
        {
            UpDown();
        }
        if (nowTurn == GameManager.Turn.GamePlay && _isCollision && _flag)
        {
            FollowPlayerBack();
            _flag = false;
        }
        time += Time.deltaTime;
        if (time > 0.3f)
        {
            _flag = true;
            time =0;
        }
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isCollision && nowTurn == GameManager.Turn.GamePlay)
        {
            _collisionPlayer = collision.gameObject;
            _isCollision = true;
        }
    }
    /// <summary>
    /// �㉺�^������������
    /// </summary>
    void UpDown()
    {
        transform.position = new Vector2(transform.position.x, gameObject.transform.position.y + Mathf.PingPong(Time.time, 1f));
    }
    /// <summary>
    /// �Ղꂢ��[�ɂ����Ƃ��Ă��Ђ�
    /// </summary>
    void FollowPlayerBack()
    {
        DOTween.Sequence().Append(transform.DOMove(new Vector3(_collisionPlayer.transform.position.x, _collisionPlayer.transform.position.y, _collisionPlayer.transform.position.z), 2f)).Play().SetAutoKill() ;
    }
    void TurnChecker()
    {
        nowTurn = _gameManager.NowTurn;
    }
}
