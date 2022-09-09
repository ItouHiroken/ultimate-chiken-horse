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
    /// è„â∫â^ìÆÇÇµë±ÇØÇÈ
    /// </summary>
    void UpDown()
    {
    }
    /// <summary>
    /// Ç’ÇÍÇ¢Ç‚Å[Ç…Ç∏Ç¡Ç∆Ç¬Ç¢ÇƒÇ≠Ç–Ç∆
    /// </summary>
    void FollowPlayerBack()
    {

    }
}
