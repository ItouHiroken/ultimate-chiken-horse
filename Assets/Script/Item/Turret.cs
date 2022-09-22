using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ItemBase
{
    [SerializeField][Tooltip("”­ŽË‚·‚éŠÔŠu")] private float _targetTime = default;
    [SerializeField][Tooltip("”­ŽËŽžŠÔ‚ð0•b‚É–ß‚·—p‚Ì‚â‚Â")] private float _currentTime = default;
    [SerializeField] GameObject _direction;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _shotPower;
    protected new void Update()
    {
        if (_gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            _currentTime += Time.deltaTime;
        }
        if (_targetTime < _currentTime)
        {
            Shot(_direction.transform);
            _currentTime = 0;
        }
    }
    void Shot(Transform dir)
    {
        GameObject a=Instantiate(_bullet, gameObject.transform.position,_bullet.transform.rotation);
        Vector3 forceDirection = dir.position - gameObject.transform.position;
        Vector3 force = _shotPower * forceDirection;

        // —Í‚ð‰Á‚¦‚éƒƒ\ƒbƒh
        Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
    }

}
