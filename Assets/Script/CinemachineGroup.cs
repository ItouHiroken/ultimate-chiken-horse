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
    [SerializeField,Tooltip("プレイヤーの位置")] List<Transform> players = new();
    [SerializeField,Tooltip("プレイヤーがシネマシーングループに入っているか")] List<bool> inCinemachine = new();

    CinemachineTargetGroup cinemachineTargetGroup;
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
        if(players[number].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.isGoal)||
            players[number].GetComponent<PlayerMove>().Score.HasFlag(PlayerState.GetScore.Death))
        {
            cinemachineTargetGroup.RemoveMember(players[number]);
            inCinemachine[number]=false;
        }
    }
}
