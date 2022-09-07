using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �v���C���[���S�[���ɓ��������ƋN���鎖�̂��܂���
/// ����
/// 1.�v���C���[�̈ړ��\�͂�D��
/// 2.�v���C���[���������Ă��邩�ǂ������f����
/// 3.�v���C���[�������Ă������Ԃ��o����
/// </summary>
public class Goal : MonoBehaviour
{
    [SerializeField][Tooltip("�|�C���g�}�l�[�W���[�ɓn���S�[�����ԃ��X�g")] private List<GameObject> goalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField][Tooltip("�Q�[���}�l�[�W���[")] GameObject _gameManager;
    [Tooltip("�^�[���`�F���W�̊֐��g����������Ƃ��Ă���")] GameManager gameManagerScript;
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
                        goalPlayers[0].gameObject.GetComponent<Player1Move>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player2")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player2Move>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player3")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player3Move>().Score |= PlayerState.GetScore.Coin;
                    }
                    else if (goalPlayers[0].name == "Player4")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player4Move>().Score |= PlayerState.GetScore.Coin;
                    }
                    break;
                case 2:
                case 3:
                case 4:
                    if (goalPlayers[0].name == "Player1")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player1Move>().Score |= PlayerState.GetScore.First;
                    }
                    else if (goalPlayers[0].name == "Player2")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player2Move>().Score |= PlayerState.GetScore.First;
                    }
                    else if (goalPlayers[0].name == "Player3")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player3Move>().Score |= PlayerState.GetScore.First;
                    }
                    else if (goalPlayers[0].name == "Player4")
                    {
                        goalPlayers[0].gameObject.GetComponent<Player4Move>().Score |= PlayerState.GetScore.First;
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
                Player1Move playerscript;
                playerscript = collision.GetComponent<Player1Move>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player2")
            {
                Player2Move playerscript;
                playerscript = collision.GetComponent<Player2Move>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player3")
            {
                Player3Move playerscript;
                playerscript = collision.GetComponent<Player3Move>();
                if (playerscript.enabled == true)
                {
                    goalPlayers.Add(collision.gameObject);
                }
                playerscript.enabled = false;
            }
            if (collision.name == "Player4")
            {
                Player4Move playerscript;
                playerscript = collision.GetComponent<Player4Move>();
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
                Player1Move playerscript;
                playerscript = collision.GetComponent<Player1Move>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player2")
            {
                Player2Move playerscript;
                playerscript = collision.GetComponent<Player2Move>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player3")
            {
                Player3Move playerscript;
                playerscript = collision.GetComponent<Player3Move>();
                playerscript.enabled = false;
            }
            if (collision.name == "Player4")
            {
                Player4Move playerscript;
                playerscript = collision.GetComponent<Player4Move>();
                playerscript.enabled = false;
            }
        }
    }
}