using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : ItemBase
{
    [SerializeField][Tooltip("���˂���Ԋu")] private float _targetTime = default;
    [SerializeField][Tooltip("���ˎ��Ԃ�0�b�ɖ߂��p�̂��")] private float _currentTime = default;
    [SerializeField] GameObject _direction;
    [SerializeField] GameObject _bullet;
    [SerializeField] float _shotPower;
    [SerializeField] string _bulletName;
    [SerializeField] AudioSource _audioSource;
    [SerializeField] AudioClip _audioClip;
    [SerializeField] GameObject _gm;
    [SerializeField] GameManager.Turn _turn;
    private void Start()
    {
        _gm = GameObject.Find("GameManager").gameObject;
        _audioSource = GetComponent<AudioSource>(); 
        _bullet = GameObject.Find(_bulletName).gameObject;
    }
    protected new void Update()
    {
        TurnChecker(_gm);
        if (_turn == GameManager.Turn.GamePlay)
        {
            _currentTime += Time.deltaTime;
        }
        
        if (_targetTime < _currentTime)
        {
            Shot(_direction.transform);
            _currentTime = 0;
        }
    }
    void TurnChecker(GameObject a)
    {
        _turn = a.GetComponent<GameManager>().NowTurn;
    }
    void Shot(Transform dir)
    {
        GameObject a=Instantiate(_bullet, gameObject.transform.position,_bullet.transform.rotation);
        Vector3 forceDirection = dir.position - gameObject.transform.position;
        Vector3 force = _shotPower * forceDirection;

        a.transform.localScale = gameObject.transform.localScale;
        // �͂������郁�\�b�h
        Rigidbody2D rb = a.gameObject.GetComponent<Rigidbody2D>();
        rb.AddForce(force, ForceMode2D.Impulse);
        _audioSource.PlayOneShot(_audioClip);
    }

}
