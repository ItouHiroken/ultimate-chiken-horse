using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : ItemBase
{
    GameManager _gameManager;
    GameObject _collisionPlayer;
    bool _isCollision = false;
    private void Update()
    {
        if (_gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            if (_isCollision)
            {
                UpDown();
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_isCollision && _gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            _collisionPlayer = collision.gameObject;
            _isCollision = true;
        }
    }
    /// <summary>
    /// 上下運動をし続ける
    /// </summary>
    void UpDown()
    {
    }
    /// <summary>
    /// ぷれいやーにずっとついてくひと
    /// </summary>
    void FollowPlayerBack()
    {

    }
}
