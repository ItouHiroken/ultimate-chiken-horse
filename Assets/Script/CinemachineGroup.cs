using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineGroup : MonoBehaviour
{
    [Header("見たいだけ変数くん")]
    [Tooltip("アクションターンが始まるときに一回シネマシーンのターゲットグループの中身を変える")]
    public bool _playerCameraReset;
    [Tooltip("アイテム選択ターンが始まるときに一回シネマシーンのターゲットグループの中身を変える")]
    public bool _cursorCameraReset;

    [Header("アサインしたいものたち")]
    [SerializeField,Tooltip("プレイヤーの位置")] List<Transform> _players = new();
    [SerializeField,Tooltip("プレイヤーがシネマシーングループに入っているか")] List<bool> _inCinemachine = new();

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
    void AddCinemachineArray(int playerNum)
    {
        _cinemachineTargetGroup.AddMember(_players[playerNum].transform, 1, 0);
        _inCinemachine[playerNum] = true;
    }
    void RemoveCinemachineArray(int playerNum)
    {
        if (_inCinemachine[playerNum])
        {
            _cinemachineTargetGroup.RemoveMember(_players[playerNum]);
            _inCinemachine[playerNum] = false;
        }
    }
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
