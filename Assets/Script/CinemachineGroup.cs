using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineGroup : MonoBehaviour
{
    CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] List<Transform> players = new();
    [SerializeField] GameManager gameManager;
    [SerializeField] List<bool> inCinemachine = new();
    public bool cameraReset;

    void Start()
    {
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        for (int i = 0; i < players.Count; i++)
        {
            AddCinemachineArray(i);
        }
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
            cameraReset = false;
        }
    }
    void Update()
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (cameraReset)
            {
                RemoveCinemachineArray(i);
                AddCinemachineArray(i);
            }
            CheckCameraForcas(i);
        }
    }
    void CheckCameraForcas(int number)
    {
        if (players[number].GetComponent<PlayerMove>().Score == PlayerState.GetScore.Death ||
            players[number].GetComponent<PlayerMove>().Score == PlayerState.GetScore.isGoal)
        {
            cinemachineTargetGroup.RemoveMember(players[number]);
        }
    }
}
