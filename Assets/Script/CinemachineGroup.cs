using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineGroup : MonoBehaviour
{
    CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] List<Transform> players = new List<Transform>();
    [SerializeField] GameObject _gameObject;
    GameManager gameManager;

    public bool cameraReset;

    [SerializeField] List<bool> inCinemachine;
    void Start()
    {
        gameManager = _gameObject.GetComponent<GameManager>();
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();

        inCinemachine = new(players.Count);//new List<bool>()‚Ì—ª
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
