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
    [SerializeField,Tooltip("�v���C���[�̈ʒu")] List<Transform> _players = new();
    [SerializeField,Tooltip("�v���C���[���V�l�}�V�[���O���[�v�ɓ����Ă��邩")] List<bool> _inCinemachine = new();

    CinemachineTargetGroup _cinemachineTargetGroup;
    void Start()
    {
        _cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        for (int i = 0; i < _players.Count; i++)
        {
            AddCinemachineArray(i);
        }
    }

    void Update()
    {
        for (int i = 0; i < _players.Count; i++)
        {
            CheckCameraForcas(i);
            if (_playerCameraReset)
            {
                Debug.Log(_players[i].name);
                RemoveCinemachineArray(i);
                AddCinemachineArray(i);
            }
        }
        _playerCameraReset = false;
    }
    /// <summary>
    /// �V�l�}�V�[���̃O���[�v�̔z��Ƀv���C���[�̃��X�g������
    /// </summary>
    /// <param name="playerNum"></param>
    void AddCinemachineArray(int playerNum)
    {
        _cinemachineTargetGroup.AddMember(_players[playerNum].transform, 1, 0);
        _inCinemachine[playerNum] = true;
    }
    /// <summary>
    /// �V�l�}�V�[���̃O���[�v�̔z��̒��g��S������
    /// </summary>
    void RemoveCinemachineArray(int playerNum)
    {
        if (_inCinemachine[playerNum])
        {
            _cinemachineTargetGroup.RemoveMember(_players[playerNum]);
            _inCinemachine[playerNum] = false;
        }
    }
    /// <summary>
    /// �v���C���[�������Ď����A�S�[���܂��̓f�X�����Ƃ��Ƀ^�[�Q�b�g�O���[�v�̔z�񂩂甲����
    /// </summary>
    /// <param name="number"></param>
    void CheckCameraForcas(int number)
    {
        if(_players[number].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal)||
            _players[number].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
        {
            _cinemachineTargetGroup.RemoveMember(_players[number]);
            _inCinemachine[number]=false;
        }
    }
}
