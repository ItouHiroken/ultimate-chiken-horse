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
    [Header("この世の総てを司る最強Enumくん")]
    public Turn NowTurn;
    [Tooltip("ターンチェンジするときのアクションデリゲート")]
    public delegate void TurnChangeAction();
    public TurnChangeAction TurnChangeActionMethod = default;
    [Header("インスタンスしたいものたち")]
    [SerializeField, Tooltip("カーソルたち")] List<GameObject> _cursolList = new();
    [SerializeField, Tooltip("プレイヤーたち")] List<GameObject> _playerList = new();
    [SerializeField, Tooltip("boolを渡したい")] StartingPoint _startingPoint;
    [SerializeField, Tooltip("boolを渡したい")] CursorStart _resetCursorPoint;
    [SerializeField, Tooltip("boolを渡したい")] SummonItem _summonItem;
    [SerializeField, Tooltip("boolを渡したい")] Goal _goal;
    [SerializeField, Tooltip("boolを渡したい")] CinemachineGroup _cinemachineGroup;
    [SerializeField, Tooltip("リザルトのプレイヤーの勝利キャンバス")] List<Canvas> _playerCanvas = new();
    [SerializeField, Tooltip("リザルトターンの時のキャンバス")] Canvas _result;
    [SerializeField, Tooltip("デバッグ用、ターンを教えてくれる")] Text _text;
    [SerializeField, Tooltip("アイテムを選択、設置する時に使うカメラ")] GameObject _itemTurnCamera;

    [Header("変数たち")]
    [SerializeField, Tooltip("勝利するスコアのライン")] int _clearLine = 100;
    [SerializeField, Tooltip("リザルトターンの時間")] float _resultTime = 5;

    [Header("ほかのところに渡したい")]
    [Tooltip("カーソルがアイテム選択したらここに追加される")] public List<GameObject> IsChoiceCursol;
    [Tooltip("カーソルが選択したアイテムが追加される")] public List<GameObject> ChoiceList = new();
    [Tooltip("カーソルがアイテム設置したらここに追加される")] public List<GameObject> IsPutCursol;

    private void Update()
    {
        //デバッグ用、今のターンを教えてくれる
        _text.text = NowTurn.ToString();
        //デバッグ用、ターンを切り替えてくれる
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TurnChange();
        }
        //もしカーソルが全員アイテムを選んだらターンが切り替わる
        if (IsChoiceCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            IsChoiceCursol.Clear();
        }
        //もしカーソルが全員アイテムを設置したらターンが切り替わる
        if (IsPutCursol.Count == Menu._playerNumber)
        {
            TurnChange();
            IsPutCursol.Clear();
        }
    }
    public void TurnChange()
    {
        Debug.Log("さっきは"+NowTurn);
        switch (NowTurn)
        {
            ///Play→Result
            case Turn.GamePlay://GamePlay終わりの時
                //リザルトのキャンバスをtrue
                _result.gameObject.SetActive(true);
                //リザルトターンの時間を制御する
                //発表会用で消してる↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓↓
                //Invoke(nameof(TurnChange), _resultTime);
                //今のターンをリザルトターンに切り替える
                NowTurn = GameManager.Turn.Result;
                TurnChangeActionMethod();
                break;

            ///Result→Select
            ///      →End
            case Turn.Result://Result終わりの時

                ///今のターンをアイテム選択ターンに切り替える
                NowTurn = GameManager.Turn.SelectItem;
                //カーソルを呼び起こす
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                //リザルトキャンバスを消す
                _result.gameObject.SetActive(false);
                //カーソルの機能の「ついてきて」のboolをfalseにする
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].GetComponent<PlayerCursor>()._isFollowing = false;
                }
                //アイテム選択、設置ターン用のカメラをtrue
                _itemTurnCamera.SetActive(true);
                //カーソルを定位置に召喚するbool
                _resetCursorPoint.GetComponent<CursorStart>().SelectSceneStart = true;
                //アイテム召喚するbool
                _summonItem.GetComponent<SummonItem>()._isChoiceItem = true;
                ////もし誰かが目標点数超えていたら
                //1.今のターンをゲームエンドに切り替える
                //2.プレイヤーの勝ったキャンバスをtrue
                for (int i = 0; i < _playerList.Count; i++)
                {
                    if (_playerList[0].GetComponent<PlayerMove>()._scorePoint >= _clearLine)
                    {
                        NowTurn = GameManager.Turn.GameEnd;
                        Debug.Log("GameEnd");
                        _playerCanvas[i].gameObject.SetActive(true);
                        TurnChangeActionMethod();
                        break;
                    }
                }
                TurnChangeActionMethod();
                break;
            ///Select→Set
            case Turn.SelectItem://Select終わりの時
                IsChoiceCursol.Clear();//カーソルの僕選びましたよリストの中身をリセットする(なくす)
                //カーソルはアイテムを選んだらカーソル自身をflaseにするけど、ターンが切り替わってまたtrueにする。
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(true);
                }
                //選んだアイテムたちをfalse
                for (int i = 0; i < ChoiceList.Count; i++)
                {
                    if (!ChoiceList[i]) { break; }
                    ChoiceList[i].SetActive(true);
                }
                //今のターンをアイテム設置ターンに切り替える
                NowTurn = GameManager.Turn.SetItem;
                TurnChangeActionMethod();
                break;

            ///Set→Play
            case Turn.SetItem:
                //カーソルはもういなくなってほしい
                for (int i = 0; i < _cursolList.Count; i++)
                {
                    _cursolList[i].SetActive(false);
                }
                //プレイヤーを元のポジションに戻すbool
                _startingPoint.GetComponent<StartingPoint>().PlaySceneStart = true;
                //シネマシーンカメラのターゲットグループにプレイヤーたちを入れるbool
                _cinemachineGroup._playerCameraReset = true;
                //デバッグ用
                IsPutCursol.Clear();
                //アイテム用カメラを一回消す
                _itemTurnCamera.SetActive(false);
                //今のターンをプレイターンに切り替える
                NowTurn = GameManager.Turn.GamePlay;
                TurnChangeActionMethod();
                break;
            case Turn.GameEnd:
                TurnChangeActionMethod();
                break;

            default:
                break;
        }
        Debug.Log("今は"+NowTurn);
    }
    /// <summary>
    /// 今のターンです
    /// </summary>
    public enum Turn
    {
        GamePlay,
        Result,
        SelectItem,
        SetItem,
        GameEnd,
    }
}
