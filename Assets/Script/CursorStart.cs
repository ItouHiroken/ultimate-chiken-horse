using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorStart : MonoBehaviour
{
    [SerializeField] GameObject _player1Point;
    [SerializeField] GameObject _player2Point;
    [SerializeField] GameObject _player3Point;
    [SerializeField] GameObject _player4Point;

    [SerializeField] GameObject _player1;
    [SerializeField] GameObject _player2;
    [SerializeField] GameObject _player3;
    [SerializeField] GameObject _player4;

    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
    public GameManager.Turn Turn;
    public bool SelectSceneStart;
    private void Start()
    {
        _player1 = GameObject.Find("Player1Cursor");
        _player2 = GameObject.Find("Player2Cursor");
        _player3 = GameObject.Find("Player3Cursor");
        _player4 = GameObject.Find("Player4Cursor");
    }
    private void Update()
    {
        TurnChecker(_gameManager);
        if (SelectSceneStart == true)
        {
            _player1.transform.position = _player1Point.transform.position;
            _player2.transform.position = _player2Point.transform.position;
            _player3.transform.position = _player3Point.transform.position;
            _player4.transform.position = _player4Point.transform.position;
            _player1.GetComponent<PlayerCursor>().enabled = true;
            _player2.GetComponent<PlayerCursor>().enabled = true;
            _player3.GetComponent<PlayerCursor>().enabled = true;
            _player4.GetComponent<PlayerCursor>().enabled = true;
            SelectSceneStart = false;
        }
    }
    public void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}
