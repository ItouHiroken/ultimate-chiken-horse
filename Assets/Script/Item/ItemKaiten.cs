using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転する角度を自分でインスペクターに入れて使おう！
/// </summary>
public class ItemKaiten : MonoBehaviour
{
    [SerializeField]int kaitenIndex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quaternion rot = Quaternion.AngleAxis(kaitenIndex, Vector3.forward);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = this.transform.rotation;
            // 合成して、自身に設定
            this.transform.rotation = q * rot;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Quaternion rot = Quaternion.AngleAxis(kaitenIndex, Vector3.back);
            // 現在の自信の回転の情報を取得する。
            Quaternion q = this.transform.rotation;
            // 合成して、自身に設定
            this.transform.rotation = q * rot;
        }
    }
}
