using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �I�΂�Ȃ������玩���̎����Ȃ���
/// </summary>
public class SelectCheck : MonoBehaviour
{
    GameManager gameManager;
    GameManager.Turn turn;
    public bool selected = false;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>().GetComponent<GameManager>();
    }
    private void Update()
    {
        turn = gameManager.NowTurn;

        if (turn == GameManager.Turn.GamePlay)
        {
            if (!selected)
            {
                Destroy(gameObject);
            }
        }
    }
}
