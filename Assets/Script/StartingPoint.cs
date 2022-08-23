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

    [SerializeField] Player1Move p1;
    //[SerializeField] Player2Move p2;
    //[SerializeField] Player3Move p3;
    //[SerializeField] Player4Move p4;
    
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
        if (Turn == GameManager.Turn.GamePlay)
        {
        }
        if (PlaySceneStart == true)
        {
            Player1.transform.position = Player1Point.transform.position;
            Player2.transform.position = Player2Point.transform.position;
            Player3.transform.position = Player3Point.transform.position;
            Player4.transform.position = Player4Point.transform.position;
            p1.enabled = true;
            //p2.enabled = true;
            //p3.enabled = true;
            //p4.enabled = true;
            PlaySceneStart = false;
        }
    }
    void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}