using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bomb : MonoBehaviour
{
    public bool _use;
    [SerializeField] GameObject _manager;
    GameManager.Turn _turn;
    CircleCollider2D _circleCollider;
    private void Start()
    {
        _circleCollider = GetComponent<CircleCollider2D>();
    }
    void Update()
    {
        TurnChecker(_manager);
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (_use && _turn == GameManager.Turn.SetItem)
        {
            _circleCollider.isTrigger = false;
            _use = false;
        }
    }
    void TurnChecker(GameObject a)
    {
        _turn = a.GetComponent<GameManager>().NowTurn;
    }
}
