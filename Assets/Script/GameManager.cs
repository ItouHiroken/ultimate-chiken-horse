using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤー人数選択フェイズ
/// 1、プレイヤー人数選択
/// 2、プレイヤー色選択
/// ゲームフェイズ
/// 1、ゲームプレイのターン
/// 全員が死んだらポイント集計のターンへ
/// 2、ポイント集計のターン
/// →→→もし誰かが目標点数達成したら、または一定ターン数経ったら終了フェイズへ
/// 3、アイテム選択ターン
/// →→→全員がアイテム選択したら、または一定時間経ったらアイテム設置ターンへ
/// 4、アイテム設置ターン
/// →→→全員がアイテム設置したら、または一定時間経ったらゲームプレイターンへ
/// 誰かが時間切れまで設置していなかったら、今の場所に設置される
/// 終了フェイズ
/// 1、勝利したひとがドアップされる
/// </summary>
public class GameManager : MonoBehaviour
{
    [SerializeField] Player1Move p1;
    //[SerializeField] Player2Move p2;
    //[SerializeField] Player3Move p3;
    //[SerializeField] Player4Move p4;
    public Turn NowTurn;
    void TurnChange()
    {
        switch (NowTurn)  
        {
            case Turn.GamePlay:
                NowTurn = GameManager.Turn.Result;
                //NowTurn = NowTurn & ~GameManager.Turn.GamePlay;
                break;
            case Turn.Result:
                NowTurn = GameManager.Turn.SelectItem;
                //NowTurn = NowTurn & ~GameManager.Turn.Result;
                break;
            case Turn.SelectItem:
                NowTurn = GameManager.Turn.SetItem;
                //NowTurn = NowTurn & ~GameManager.Turn.SelectItem;
                break;
            case Turn.SetItem:
                NowTurn = GameManager.Turn.GamePlay;
                //NowTurn = NowTurn & ~GameManager.Turn.SetItem;
                break;

            default:
                break;
        }
    }
    public enum Turn
    {
        GamePlay,
        Result,
        SelectItem,
        SetItem,
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TurnChange();
        }
        //if (p1.enabled == false/*&& p2.enabled == false&&p3.enabled == false&&p4.enabled == false*/)
        //{
        //    TurnChange();
        //}
    }
}
