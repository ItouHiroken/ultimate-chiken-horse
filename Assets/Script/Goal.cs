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
    [Tooltip("�|�C���g�}�l�[�W���[�ɓn���S�[�����ԃ��X�g")] public List<GameObject> GoalPlayers = new List<GameObject>(Menu._playerNumber);
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[")] GameManager _gameManager;
    [SerializeField, Tooltip("�v���C���[�̃��X�g")] List<PlayerMove> _players;
    [Tooltip("�^�[���`�F���W�̊֐��g����������Ƃ��Ă���")] GameManager _gameManagerScript;
    [SerializeField] PointManager _pointManager;
    void Start()
    {
        _gameManagerScript = _gameManager.GetComponent<GameManager>();
    }

    void Update()
    {
        if (_gameManagerScript.NowTurn == GameManager.Turn.GamePlay)
        {
            //�S�l�̃v���C���[���S���S�[���܂��̓f�X��ԂɂȂ�����A
            //��l�����S�[���������ǂ����̃`�F�b�N
            //��ʂ̃`�F�b�N
            //�S���S�[�����Ă邩�̃`�F�b�N������
            if ((_players[0].Score.HasFlag(PlayerState.GetScore.isGoal) || _players[0].Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[1].Score.HasFlag(PlayerState.GetScore.isGoal) || _players[1].Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[2].Score.HasFlag(PlayerState.GetScore.isGoal) || _players[2].Score.HasFlag(PlayerState.GetScore.Death))
            && (_players[3].Score.HasFlag(PlayerState.GetScore.isGoal) || _players[3].Score.HasFlag(PlayerState.GetScore.Death)))
            {
                switch (GoalPlayers.Count)
                {
                    case 1:
                        GoalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.Solo;

                        Debug.Log(GoalPlayers[0] + "����l�����S�[��");
                        break;
                    case 2:
                    case 3:
                        GoalPlayers[0].gameObject.GetComponent<PlayerMove>().Score |= PlayerState.GetScore.First;
                        Debug.Log(GoalPlayers[0].name + "�����");
                        break;
                    case 4:
                        for (int i = 0; i < GoalPlayers.Count; i++)
                        {
                            GoalPlayers[i].GetComponent<PlayerMove>().Score = 0;
                        }
                        Debug.Log("�S���S�[����������|�C���g�͑����Ȃ���");
                        break;
                    default:
                        break;
                }
                //�����œ��_Enum��ύX������Ƀ|�C���g�}�l�[�W���[�ɓ_���m�F���Ă��炤
                _pointManager._isCheck = true;
                //�^�[���̐؂�ւ�  
                _gameManager.TurnChange();
            }
        }
    }
    /// <summary>
    /// ���̃v���C���[�̓S�[�������惊�X�g�ɒǉ�����
    /// �v���C���[�ɐG�ꂽ�瓮�����~�߂�
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
    /// �v���C���[�ɐG�ꂽ�瓮�����~�߂�
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