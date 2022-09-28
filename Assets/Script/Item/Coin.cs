using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class Coin : ItemBase
{
    GameObject _collisionPlayer;
    [SerializeField] bool _isCollision = false;
    [SerializeField] bool _flag;
    [SerializeField] bool _isUsed;
    [SerializeField] CircleCollider2D _circleCollider;
    float time; 

    protected new void Update()
    {
        base.Update();
        if (!_isUsed)
        {
            if (_nowTurn == GameManager.Turn.GamePlay)
            {
                _circleCollider.isTrigger = true;
                gameObject.tag = "Coin";
            }
            else { _circleCollider.isTrigger = false; }
            if (_nowTurn == GameManager.Turn.GamePlay && _isCollision)
            {
                UpDown();
            }
            if (_nowTurn == GameManager.Turn.GamePlay && _isCollision && _flag)
            {
                if (_collisionPlayer != null)
                {
                    FollowPlayerBack();
                }
                _flag = false;
            }
            time += Time.deltaTime;
            if (time > 0.3f)
            {
                _flag = true;
                time = 0;
            }
        }
    }
   protected new void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (!_isCollision && _nowTurn == GameManager.Turn.GamePlay)
        {
            _collisionPlayer = collision.gameObject;
            _isCollision = true;
        }

        if (_nowTurn == GameManager.Turn.GamePlay && collision.gameObject.name == "Goal")
        {
            _isUsed = true;
            this.gameObject.transform.position = new Vector3(1000,1000,1000);
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
        DOTween.Sequence().Append(transform.DOMove(new Vector3(_collisionPlayer.transform.position.x, _collisionPlayer.transform.position.y + 3, _collisionPlayer.transform.position.z), 2f)).Play().SetAutoKill();
    }
}
