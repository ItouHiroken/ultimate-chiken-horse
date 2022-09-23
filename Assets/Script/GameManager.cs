using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
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
    [Header("この世の総てを支配する最強Enumくん")]
    public Turn NowTurn;

    [Header("インスタンスしたいものたち")]
    [SerializeField] List<GameObject> _cursolList = new();
    [SerializeField] List<GameObject> _playerList = new();
    [SerializeField] List<Canvas> _playerCanvas = new();
    [SerializeField] GameObject _startingPoint;
    [SerializeField] GameObject _resetCursorPoint;
    [SerializeField] GameObject _summonItem;
    [SerializeField] GameObject _goal;
    [SerializeField] CinemachineGroup _cinemachineGroup;
    [SerializeField] Canvas _result;
    [SerializeField] Text _text;
    [SerializeField] GameObject _itemTurnCamera;

    

    [Header("変数たち")]
    [SerializeField] int _clearLine = 100;
    [SerializeField] float _TurnChangeTime = 5;
    [SerializeField] float _CountChangeTime;

    [Header("ほかのところに渡したい")]
    public List<GameObject> _isChoiceCursol;
    public List<GameObject> _isPutCursol;
    public List<GameObject> _choiceList = new();




    private void Update()
    {
        _text.text = NowTurn.ToString();
        _CountChangeTime += Time.deltaTime;
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
        if (NowTurn == Turn.Result)
        {
            if (_CountChangeTime >= _TurnChangeTime) TurnChange();
        }
    }
    public void TurnChange()
    {
        switch (NowTurn)
        {
            case Turn.GamePlay://GamePlay終わりの時

                _result.gameObject.SetActive(true);
                _CountChangeTime = 0;

                NowTurn = GameManager.Turn.Result;
                break;
            case Turn.Result://Result終わりの時
                NowTurn = GameManager.Turn.SelectItem;
                
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                _result.gameObject.SetActive(false);
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].GetComponent<PlayerCursor>()._isFollowing = false; 
                }
                _itemTurnCamera.SetActive(true);
                _resetCursorPoint.GetComponent<CursorStart>().SelectSceneStart = true;
                _summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                for (int i = 0; i < _playerList.Count; i++)
                {
                    if (_playerList[0].GetComponent<PlayerMove>()._scorePoint >= _clearLine)
                    {
                        NowTurn = GameManager.Turn.GameEnd;
                        Debug.Log("GameEnd");
                        _playerCanvas[i].gameObject.SetActive(true);
                        break;
                    }
                }
                break;
            case Turn.SelectItem://Select終わりの時
                _isChoiceCursol.Clear();//上にも同じこと書いてあるけど、デバッグ用
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                for (int i = 0; i < _choiceList.Count; i++)
                {
                    _choiceList[i].SetActive(true);
                }
                NowTurn = GameManager.Turn.SetItem;
                break;
            case Turn.SetItem://Set終わりの時
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(false);
                }
                _startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                _cinemachineGroup._playerCameraReset = true;
                _isPutCursol.Clear();//デバッグ用
                _itemTurnCamera.SetActive(false);
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
