using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// 回ります
/// </summary>
public class Guruguru : MonoBehaviour
{
    Transform target;   // 移動対象オブジェクト
    [SerializeField]Transform origin;   // 公転原点
    float prevVal;      // 前回の角度

    private void Start()
    {
        target = transform;
    }
    public Tween DoRotateAround(float endValue, float duration)
    {
        prevVal = 0.0f;

        // durationの時間で値を0～endValueまで変更させて公転処理を呼ぶ
        Tween ret = DOTween.To(x => RotateAroundPrc(x), 0.0f, endValue, duration);

        return ret;
    }

    /// <summary>
    /// 公転処理
    /// </summary>
    /// <param name="value"></param>
    private void RotateAroundPrc(float value)
    {
        // 前回との差分を計算
        float delta = value - prevVal;

        // Y軸周りに公転運動
        target.RotateAround(origin.position, Vector3.up, delta);

        // 前回の角度を更新
        prevVal = value;
    }
}
