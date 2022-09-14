using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartingPoint : MonoBehaviour
{
    [SerializeField] GameObject Player1Point;
    [SerializeField] GameObject Player2Point;
    [SerializeField] GameObject Player3Point;
    [SerializeField] GameObject Player4Point;

    [SerializeField] GameObject Player1;
    [SerializeField] GameObject Player2;
    [SerializeField] GameObject Player3;
    [SerializeField] GameObject Player4;

    [SerializeField] PlayerMove p1;
    [SerializeField] PlayerMove p2;
    [SerializeField] PlayerMove p3;
    [SerializeField] PlayerMove p4;

    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
    public GameManager.Turn Turn;
    public bool PlaySceneStart;
    private void Start()
    {
        Player1 = GameObject.Find("Player1");
        Player2 = GameObject.Find("Player2");
        Player3 = GameObject.Find("Player3");
        Player4 = GameObject.Find("Player4");
    }
    private void Update()
    {
        TurnChecker(_gameManager);
        if (PlaySceneStart == true)
        {
            Player1.transform.position = Player1Point.transform.position;
            Player2.transform.position = Player2Point.transform.position;
            Player3.transform.position = Player3Point.transform.position;
            Player4.transform.position = Player4Point.transform.position;
            Player1.GetComponent<PlayerMove>().enabled = true;
            Player2.GetComponent<PlayerMove>().enabled = true;
            Player3.GetComponent<PlayerMove>().enabled = true;
            Player4.GetComponent<PlayerMove>().enabled = true;
            PlaySceneStart = false;
        }
    }
    public void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}