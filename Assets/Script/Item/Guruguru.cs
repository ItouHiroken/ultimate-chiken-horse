using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回ります
/// </summary>
public class Guruguru : ItemBase
{
    [SerializeField]float _kaitenSpeed;
    protected new void Update()
    {
        base.TurnChecker();
        if(base._nowTurn==GameManager.Turn.GamePlay)
        {
            Mawaru();
        } 
        base.Update();
    }
    void Mawaru()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.back);
        // 現在の自信の回転の情報を取得する。
        Quaternion q = gameObject.transform.rotation;
        // 合成して、自身に設定
        gameObject.transform.rotation = q * rot;
    }
}
