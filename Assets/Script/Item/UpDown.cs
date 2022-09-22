using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class UpDown: ItemBase
{
    [SerializeField] float _speed;
    [SerializeField] float _distans;
    [SerializeField] GameManager.Turn _turn;
    [SerializeField] Vector3 _moveDirection = Vector3.up + Vector3.right;
    [SerializeField] float _moveSeconds;

    [SerializeField] Vector2 _kokomade;
    public float nowPosi;

    void Start()
    {
        nowPosi = this.transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        TurnChecker();
        if (_turn == GameManager.Turn.GamePlay)
        {
            Move();
        }
    }
    void Move()
    {
        transform.position = new Vector3(transform.position.x, nowPosi + Mathf.PingPong(Time.time/_speed, _distans), transform.position.z);
    }
    void TurnChecker()
    {
        _turn = base._gameManager.NowTurn;
    }
}
