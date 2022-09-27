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
    [SerializeField][Tooltip("�|�C���g�}�l�[�W���[�ɓn���S�[�����ԃ��X�g")] public List<GameObject> goalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField][Tooltip("�Q�[���}�l�[�W���[")] GameObject _gameManager;
    [SerializeField] List<GameObject> _players;
    [Tooltip("�^�[���`�F���W�̊֐��g����������Ƃ��Ă���")] GameManager _gameManagerScript;
    [SerializeField] PointManager _pointManager;
    void Start()
    {
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
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
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;

                        Debug.Log(goalPlayers[0] + "����l�����S�[��");
                        break;
                    case 2:
                    case 3:
                        goalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        Debug.Log(goalPlayers[0].name + "�����");
                        break;
                    case 4:
                        for (int i = 0; i < goalPlayers.Count; i++)
                        {
                            goalPlayers[i].GetComponent<PlayerMove>().Score = 0;
                        }
                        Debug.Log("�S���S�[����������|�C���g�͑����Ȃ���");
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
            PlayerMove playerscript;
            playerscript = collision.GetComponent<PlayerMove>();
            if (playerscript.enabled == true)
            {
                goalPlayers.Add(collision.gameObject);
            }
            playerscript.enabled = false;

        }
    }
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