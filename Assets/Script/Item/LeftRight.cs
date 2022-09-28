using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LeftRight : ItemBase
{
    [SerializeField] float _speed;
    [SerializeField] float _distans;
    [SerializeField] Vector3 _moveDirection = Vector3.up + Vector3.right;
    [SerializeField] float _moveSeconds;

    [SerializeField] Vector2 _kokomade;
    public float nowPosi;

    protected new void Start()
    {
        base.Start();
        nowPosi = this.transform.position.x;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        if (_nowTurn == GameManager.Turn.GamePlay)
        {
            Move();
        }
    }
    void Move()
    {
        transform.position = new Vector3(nowPosi + Mathf.PingPong(Time.time / _speed, _distans), transform.position.y, transform.position.z);
    }
}
