using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

    [SerializeField] GameObject p1Cursol;

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject summonItem;

    [SerializeField] Player1Move player1;
    [SerializeField] Canvas Result;
    [SerializeField] int clearLine = 10;

    [SerializeField] public List<GameObject> _isChoiceCursol;
    [SerializeField] public List<GameObject> _isPutCursol;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TurnChange();
        }
        if (_isChoiceCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            _isChoiceCursol.Clear();
        }
        if (_isPutCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            _isPutCursol.Clear();
        }

    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay:
                p1Cursol.SetActive(false);
                Result.gameObject.SetActive(true);
                NowTurn = GameManager.Turn.Result;
                break;
            case Turn.Result:
                NowTurn = GameManager.Turn.SelectItem;
                p1Cursol.SetActive(true);
                Result.gameObject.SetActive(false);
                if (player1.GetComponent<Player1Move>().P1Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                break;
            case Turn.SelectItem:

                NowTurn = GameManager.Turn.SetItem;
                break;
            case Turn.SetItem:
                NowTurn = GameManager.Turn.GamePlay;
                startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                break;
            case Turn.GameEnd:

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
        GameEnd,
    }
}
