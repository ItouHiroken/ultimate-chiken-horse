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
    [SerializeField][Tooltip("�Q�[���}�l�[�W���[")]GameObject _gameManager;
    [Tooltip("�^�[���`�F���W�̊֐��g����������Ƃ��Ă���")]GameManager gameManagerScript;
    int playerCount;

    void Start()
    {
        gameManagerScript = _gameManager.GetComponent<GameManager>();
        playerCount = Menu._playerNumber;
    }

    //void Update()
    //{
    //    if (goalPlayers.Count == playerCount)
    //    {
    //        gameManagerScript.TurnChange();
    //        goalPlayers.Clear();
    //    }
    //}
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
                  //  playerscript.isGoal1 = true;
                }
                playerscript.enabled = false;

                ///��������œn���ʂ����߂�A����̓|�C���g���Ǘ�����X�N���v�g������Ă���l���悤�B
                //if (playerscript.isDead == true)///�v���C���[���������u�ԁA����ł����ꍇ�|�C���g������
                //{

                //}
                //else///�����Ă����葽���̃|�C���g����ɓ���
                //{

                //}
                //if(playerscript.)
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
               // GameObject obj = GameObject.Find("Player1");
                playerscript = collision.GetComponent<Player1Move>();
                playerscript.enabled = false;
            }
            //    if (other.name == "Player2")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player2");
            //        playerscript = obj.GetComponent<Player2Move>();
            //        playerscript.enabled = false;
            //    }
            //    if (other.name == "Player3")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player3");
            //        playerscript = obj.GetComponent<Player3Move>();
            //        playerscript.enabled = false;
            //    }
            //    if (other.name == "Player4")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player4");
            //        playerscript = obj.GetComponent<Player4Move>();
            //        playerscript.enabled = false;
            //    }
        }
    }
}