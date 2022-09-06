using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
public class CinemachineGroup : MonoBehaviour
{
    CinemachineTargetGroup cinemachineTargetGroup;

    [SerializeField] private List<Transform> players = new List<Transform>();
    [SerializeField] GameObject _gameObject;
    GameManager gameManager;

    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] GameObject P3;
    [SerializeField] GameObject P4;

    public bool cameraReset;
    void Start()
    {
        gameManager = _gameObject.GetComponent<GameManager>();
        cinemachineTargetGroup = GetComponent<CinemachineTargetGroup>();
    }
    void Update()
    {
        if (gameManager.NowTurn == GameManager.Turn.GamePlay)
        {
            if (cameraReset)
            {
                cinemachineTargetGroup.RemoveMember(players[0]);
                cinemachineTargetGroup.RemoveMember(players[1]);
                cinemachineTargetGroup.RemoveMember(players[2]);
                cinemachineTargetGroup.RemoveMember(players[3]);

                cinemachineTargetGroup.AddMember(P1.transform, 1, 0);
                cinemachineTargetGroup.AddMember(P2.transform, 1, 0);
                cinemachineTargetGroup.AddMember(P3.transform, 1, 0);
                cinemachineTargetGroup.AddMember(P4.transform, 1, 0);
            }

            if (P1.GetComponent<Player1Move>().Score == PlayerState.GetScore.Death || P1.GetComponent<Player1Move>().Score == PlayerState.GetScore.isGoal)
            {
                Debug.Log("AAA");
                cinemachineTargetGroup.RemoveMember(players[0]);
            }
            if (P2.GetComponent<Player2Move>().Score == PlayerState.GetScore.Death || P2.GetComponent<Player2Move>().Score == PlayerState.GetScore.isGoal)
            {
                cinemachineTargetGroup.RemoveMember(players[1]);
            }
            if (P3.GetComponent<Player3Move>().Score == PlayerState.GetScore.Death || P3.GetComponent<Player3Move>().Score == PlayerState.GetScore.isGoal)
            {
                cinemachineTargetGroup.RemoveMember(players[2]);
            }
            if (P4.GetComponent<Player4Move>().Score == PlayerState.GetScore.Death || P4.GetComponent<Player4Move>().Score == PlayerState.GetScore.isGoal)
            {
                cinemachineTargetGroup.RemoveMember(players[3]);
            }
        }
    }
}
