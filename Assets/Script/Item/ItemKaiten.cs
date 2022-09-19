using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 回転する角度を自分でインスペクターに入れて使おう！
/// 修正したいこと：こいつを選択したひとがボタン押してないと回転しないようにする
/// </summary>
public class ItemKaiten : MonoBehaviour
{
    [SerializeField] public int _kaitenIndex;
}
