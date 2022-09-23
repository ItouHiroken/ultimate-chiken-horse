using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class LeftRight : ItemBase
{
    [SerializeField] float _speed;
    [SerializeField] float _distans;
    [SerializeField] GameObject _manager;
    [SerializeField] GameManager.Turn _turn;
    [SerializeField] Vector3 _moveDirection = Vector3.up + Vector3.right;
    [SerializeField] float _moveSeconds;

    [SerializeField] Vector2 _kokomade;
    public float nowPosi;

    void Start()
    {
        _manager = GameObject.Find("GameManager").gameObject;
        nowPosi = this.transform.position.x;
    }

    // Update is called once per frame
    protected new void Update()
    {
        TurnChecker();
        if (_turn == GameManager.Turn.GamePlay)
        {
            Move();
        }
    }
    void Move()
    {
        transform.position = new Vector3(nowPosi + Mathf.PingPong(Time.time/_speed, _distans), transform.position.y, transform.position.z);
    }
    void TurnChecker()
    {
        _turn =_manager.GetComponent<GameManager>().NowTurn;
    }
}
