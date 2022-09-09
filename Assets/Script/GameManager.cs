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
    public Turn NowTurn;
    
    [SerializeField] GameObject p1Cursol;

    [SerializeField] GameObject startingPoint;
    [SerializeField] GameObject summonItem;

    [SerializeField] GameObject pointManager;

    [SerializeField] Player1Move player1;
    [SerializeField] Player2Move player2;
    [SerializeField] Player3Move player3;
    [SerializeField] Player4Move player4;
    [SerializeField] Canvas Result;
    [SerializeField] int clearLine = 100;
    [SerializeField] float TurnChangeTime=5;
    [SerializeField] float CountChangeTime;
    [SerializeField] public List<GameObject> _isChoiceCursol;
    [SerializeField] public List<GameObject> _isPutCursol;
    [SerializeField] CinemachineGroup cinemachineGroup;

    [SerializeField] Text text;
    private void Update()
    { 
        text.text =NowTurn.ToString();
        CountChangeTime += Time.deltaTime;
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

        if (NowTurn == Turn.GamePlay)
        {
            if ((player1.Score.HasFlag(PlayerState.GetScore.isGoal) || player1.Score.HasFlag(PlayerState.GetScore.Death))
            && (player2.Score.HasFlag(PlayerState.GetScore.isGoal) || player2.Score.HasFlag(PlayerState.GetScore.Death))
            && (player3.Score.HasFlag(PlayerState.GetScore.isGoal) || player3.Score.HasFlag(PlayerState.GetScore.Death))
            && (player4.Score.HasFlag(PlayerState.GetScore.isGoal) || player4.Score.HasFlag(PlayerState.GetScore.Death)))
            {
                TurnChange();
            }
        }
        if (NowTurn == Turn.Result)
        {
            if (CountChangeTime >= TurnChangeTime) TurnChange();
        }
    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay:
                p1Cursol.SetActive(false);
                Result.gameObject.SetActive(true);
                pointManager.GetComponent<PointManager>()._isCheck = true;
                CountChangeTime = 0;
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
                if (player2.GetComponent<Player2Move>().P2Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                if (player3.GetComponent<Player3Move>().P3Score >= clearLine)
                {
                    NowTurn = GameManager.Turn.GameEnd;
                    Debug.Log("GameEnd");
                    break;
                }
                if (player4.GetComponent<Player4Move>().P4Score >= clearLine)
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
                startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                cinemachineGroup.cameraReset = true;
                NowTurn = GameManager.Turn.GamePlay;
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
