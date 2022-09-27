using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーがゴールに入ったあと起こる事のさまざま
/// メモ
/// 1.プレイヤーの移動能力を奪う
/// 2.プレイヤーが生存しているかどうか判断する
/// 3.プレイヤーが入ってきた順番を覚える
/// </summary>
public class Goal : MonoBehaviour
{
    [Tooltip("ポイントマネージャーに渡すゴール順番リスト")] public List<GameObject> GoalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField,Tooltip("ゲームマネージャー")] GameObject _gameManager;
    [SerializeField,Tooltip("プレイヤーのリスト")] List<GameObject> _players;
    [Tooltip("ターンチェンジの関数使いたいからとってくる")] GameManager _gameManagerScript;
    [SerializeField] PointManager _pointManager;
    void Start()
    {
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (_gameManager.GetComponent<GameManager>().NowTurn == GameManager.Turn.GamePlay)
        {
            //４人のプレイヤーが全員ゴールまたはデス状態になったら、
            //一人だけゴールしたかどうかのチェック
            //一位のチェック
            //全員ゴールしてるかのチェックをする
            if ((_players[0].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[0].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[1].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[1].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[2].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[2].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[3].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[3].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death)))
            {
                switch (GoalPlayers.Count)
                {
                    case 1:
                        GoalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;

                        Debug.Log(GoalPlayers[0] + "が一人だけゴール");
                        break;
                    case 2:
                    case 3:
                        GoalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        Debug.Log(GoalPlayers[0].name + "が一位");
                        break;
                    case 4:
                        for (int i = 0; i < GoalPlayers.Count; i++)
                        {
                            GoalPlayers[i].GetComponent<PlayerMove>().Score = 0;
                        }
                        Debug.Log("全員ゴールしたからポイントは増えないよ");
                        break;
                    default:
                        break;
                }
                //ここで得点Enumを変更した後にポイントマネージャーに点数確認してもらう
                _pointManager._isCheck = true;
                //ターンの切り替え  
                _gameManager.GetComponent<GameManager>().TurnChange();
            }
        }
    }
    /// <summary>
    /// このプレイヤーはゴールしたよリストに追加する
    /// プレイヤーに触れたら動きを止める
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove playerscript;
            playerscript = collision.GetComponent<PlayerMove>();
            if (playerscript.enabled == true)
            {
                GoalPlayers.Add(collision.gameObject);
            }
            playerscript.enabled = false;

        }
    }
    /// <summary>
    /// プレイヤーに触れたら動きを止める
    /// </summary>
    /// <param name="collision"></param>

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            PlayerMove playerscript;
            playerscript = collision.GetComponent<PlayerMove>();
            playerscript.enabled = false;
        }
    }
}