using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回ります
/// </summary>
public class Guruguru : ItemBase
{
    GameObject _manager;
    GameManager.Turn _turn;
    [SerializeField]float _kaitenSpeed;
    private void Start()
    {
        _manager = GameObject.Find("GameManager").gameObject;
    }
    protected new void Update()
    {
        TurnChecker(_manager);
        if(_turn==GameManager.Turn.GamePlay)
        {
            Mawaru();
        } 
    }
    void Mawaru()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.back);
        // 現在の自信の回転の情報を取得する。
        Quaternion q = gameObject.transform.rotation;
        // 合成して、自身に設定
        gameObject.transform.rotation = q * rot;
    }
    void TurnChecker(GameObject a)
    {
        _turn = a.GetComponent<GameManager>().NowTurn;
    }
}
