using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがアイテム選択画面に来た時使うカーソル 
/// </summary>
public class Player1Cursor : MonoBehaviour
{
    [Tooltip("移動速度")] public float _speed = 10.0f;
    [SerializeField][Tooltip("ゲームマネージャーから参照したい")]GameObject _gameManager;
    public  GameManager.Turn Turn;
    private Rigidbody2D rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    private void Update()
    {
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.SetItem || Turn == GameManager.Turn.SelectItem)
        {
            float verticalInput = _speed * Input.GetAxisRaw("Vertical");
            float horizontalInput = _speed * Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(horizontalInput, verticalInput);
        }
    }

    void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}