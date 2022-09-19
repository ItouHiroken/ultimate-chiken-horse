using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineGroup : MonoBehaviour
{
    [Header("�����������ϐ�����")]
    [Tooltip("�A�N�V�����^�[�����n�܂�Ƃ��Ɉ��V�l�}�V�[���̃^�[�Q�b�g�O���[�v�̒��g��ς���")]
    public bool _playerCameraReset;
    [Tooltip("�A�C�e���I���^�[�����n�܂�Ƃ��Ɉ��V�l�}�V�[���̃^�[�Q�b�g�O���[�v�̒��g��ς���")]
    public bool _cursorCameraReset;

    [Header("�A�T�C�����������̂���")]
    [SerializeField,Tooltip("�v���C���[�̈ʒu")] List<Transform> players = new();
    [SerializeField,Tooltip("�v���C���[���V�l�}�V�[���O���[�v�ɓ����Ă��邩")] List<bool> inCinemachine = new();

    CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField,Tooltip("���_")] GameObject _pointO;

    void Start()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        for (int i = 0; i < players.Count; i++)
        {
            AddCinemachineArray(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            CheckCameraForcas(i);
            if (_playerCameraReset)
            {
                Debug.Log(players[i].name);
                RemoveCinemachineArray(i);
                AddCinemachineArray(i);
            }
            if (_cursorCameraReset)
            {
                RemoveCinemachineArray(i);
                cinemachineTargetGroup.AddMember(_pointO.transform,0,0);
                inCinemachine[0] = true;
                _cursorCameraReset = false;
            }
        }
        _playerCameraReset = false;
    }
    void AddCinemachineArray(int playerNum)
    {
        cinemachineTargetGroup.AddMember(players[playerNum].transform, 1, 0);
        inCinemachine[playerNum] = true;
    }
    void RemoveCinemachineArray(int playerNum)
    {
        if (inCinemachine[playerNum])
        {
            cinemachineTargetGroup.RemoveMember(players[playerNum]);
            inCinemachine[playerNum] = false;
        }
    }
    void CheckCameraForcas(int number)
    {
        if (players[number].GetComponent<PlayerMove>().Score == PlayerState.GetScore.Death ||
            players[number].GetComponent<PlayerMove>().Score == PlayerState.GetScore.isGoal)
        {
            cinemachineTargetGroup.RemoveMember(players[number]);
            inCinemachine[number]=false;
        }
    }
}
