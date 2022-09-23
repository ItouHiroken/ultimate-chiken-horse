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
    [SerializeField][Tooltip("ポイントマネージャーに渡すゴール順番リスト")] public List<GameObject> goalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField][Tooltip("ゲームマネージャー")] GameObject _gameManager;
    [SerializeField] List<GameObject> _players;
    [Tooltip("ターンチェンジの関数使いたいからとってくる")] GameManager gameManagerScript;
    [SerializeField] PointManager _pointManager;
    int playerCount;
    bool _soloCheck;
    void Start()
    {
        gameManagerScript = _gameManager.GetComponent<GameManager>();
        playerCount = Menu._playerNumber;
    }

    void Update()
    {
        if (_gameManager.GetComponent<GameManager>().NowTurn == GameManager.Turn.GamePlay)
        {
            if ((_players[0].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[0].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[1].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[1].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[2].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[2].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[3].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal) || _players[3].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death)))
            {
                switch (goalPlayers.Count)
                {
                    case 1:
                        if (goalPlayers[0].name == "Player1")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;
                        }
                        else if (goalPlayers[0].name == "Player2")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;
                        }
                        else if (goalPlayers[0].name == "Player3")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;
                        }
                        else if (goalPlayers[0].name == "Player4")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;
                        }
                        Debug.Log(goalPlayers[0] + "が一人だけゴール");
                        break;
                    case 2:
                    case 3:
                        if (goalPlayers[0].name == "Player1")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        }
                        else if (goalPlayers[0].name == "Player2")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        }
                        else if (goalPlayers[0].name == "Player3")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        }
                        else if (goalPlayers[0].name == "Player4")
                        {
                            goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        }
                        Debug.Log(goalPlayers[0].name + "が一位");
                        break;
                    case 4:
                        for (int i = 0; i < goalPlayers.Count; i++)
                        {
                            goalPlayers[i].GetComponent<PlayerMove>().Score = 0;
                        }
                        Debug.Log("全員ゴールしたからポイントは増えないよ");

                        break;
                    default:
                        break;        
                }
                _pointManager._isCheck = true;
                _gameManager.GetComponent<GameManager>().TurnChange();
                goalPlayers.Clear();
            }
        }
    }
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.name == "Player1")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player2")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player3")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player4")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
        }
    }
    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.name == "Player1")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player2")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player3")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player4")
            {
                PlayerMove playerscript;
                playerscript = collision.GetComponent<PlayerMove>();
                playerscript.enabled = false;
            }
        }
    }
}