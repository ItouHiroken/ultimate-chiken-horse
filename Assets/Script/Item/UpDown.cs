using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpDown: ItemBase
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
        nowPosi = this.transform.position.y;
    }

    // Update is called once per frame
    protected new void Update()
    {
        base.Update();
        base.TurnChecker();
        if (_nowTurn == GameManager.Turn.GamePlay)
        {
            Move();
        }
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/_speed, _distans), transform.position.z);
    }
}
