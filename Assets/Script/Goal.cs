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
    [SerializeField][Tooltip("ポイントマネージャーに渡すゴール順番リスト")] private List<GameObject> goalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField][Tooltip("ゲームマネージャー")] GameObject _gameManager;
    [Tooltip("ターンチェンジの関数使いたいからとってくる")] GameManager gameManagerScript;
    [SerializeField] PointManager _pointManager;
    int playerCount;

    void Start()
    {
        gameManagerScript = _gameManager.GetComponent<GameManager>();
        playerCount = Menu._playerNumber;
    }

    void Update()
    {
        if (goalPlayers.Count == playerCount)
        {
            switch (goalPlayers.Count)
            {
                case 1:
                    if (goalPlayers[0].name == "Player1")
                    {
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player2")
                    {
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player3")
                    {
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player4")
                    {
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Coin;
                    }
                    break;
                case 2:
                case 3:
                case 4:
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
                    break;
                default:
                    break;
            }
            _pointManager._isCheck = true;
            goalPlayers.Clear();
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