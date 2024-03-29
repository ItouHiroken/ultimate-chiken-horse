using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ItemBase
{
    [SerializeField][Tooltip("発射する間隔")] private float _targetTime = default;
    [SerializeField][Tooltip("発射時間を0秒に戻す用のやつ")] private float _currentTime = default;
    [SerializeField] GameObject _direction;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _shotPower;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    protected new void Start()
    {
        base.Start();
        _audioSource = GetComponent<AudioSource>(); 
    }
    protected new void Update()
    {
        base.Update();
        if (base._nowTurn == GameManager.Turn.GamePlay&&gameObject.CompareTag("isChoice"))
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

        a.transform.localScale = gameObject.transform.localScale;
        // 力を加えるメソッド
        Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(_audioClip);
    }

}
