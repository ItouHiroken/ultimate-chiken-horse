using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class PointPlus : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField]GameManager.Turn Turn;
    PlayerState.GetScore Score;
    [SerializeField] Player1Move player1;
    [SerializeField] Player2Move player2;
    [SerializeField] Player3Move player3;
    [SerializeField] Player4Move player4;
    public bool pointPlusBool=true;
    private void Update()
    {
        Turn = gameManager.NowTurn;
        if (Turn == GameManager.Turn.Result&&pointPlusBool)
        {
            Debug.Log("‚Ú‚­‚¢‚Ü‚©‚ç‚Æ‚­‚Ä‚ñ‚¯‚¢‚³‚ñ‚µ‚Ü‚·");
            if (player1.Score.HasFlag(GetScore.isGoal))
            {
                if (player1.Score.HasFlag(GetScore.Death))
                {
                    player1.P1Score += 10;
                    player1.Score = 0;
                }
                else
                {
                    if (player1.Score.HasFlag(GetScore.First))
                    {
                        player1.P1Score += 10;
                    }
                    if (player1.Score.HasFlag(GetScore.Solo))
                    {
                        player1.P1Score += 15;
                    }
                    if (player1.Score.HasFlag(GetScore.Coin))
                    {
                        player1.P1Score += 10;
                    }
                    player1.Score = 0;
                }
            }
            else
            {
                player1.Score = 0;
            }
            if (player2.Score.HasFlag(GetScore.isGoal))
            {
                if (player2.Score.HasFlag(GetScore.Death))
                {
                    player2.P2Score += 10;
                    player2.Score = 0;
                }
                else
                {
                    if (player2.Score.HasFlag(GetScore.First))
                    {
                        player2.P2Score += 10;
                    }
                    if (player2.Score.HasFlag(GetScore.Solo))
                    {
                        player2.P2Score += 15;
                    }
                    if (player2.Score.HasFlag(GetScore.Coin))
                    {
                        player2.P2Score += 10;
                    }
                    player2.Score = 0;
                }
            }
            else
            {
                player2.Score = 0;
            }
            if (player3.Score.HasFlag(GetScore.isGoal))
            {
                if (player3.Score.HasFlag(GetScore.Death))
                {
                    player3.P3Score += 10;
                    player3.Score = 0;
                }
                else
                {
                    if (player3.Score.HasFlag(GetScore.First))
                    {
                        player3.P3Score += 10;
                    }
                    if (player3.Score.HasFlag(GetScore.Solo))
                    {
                        player3.P3Score += 15;
                    }
                    if (player3.Score.HasFlag(GetScore.Coin))
                    {
                        player3.P3Score += 10;
                    }
                    player3.Score = 0;
                }
            }
            else
            {
                player3.Score = 0;
            }
            if (player4.Score.HasFlag(GetScore.isGoal))
            {
                if (player4.Score.HasFlag(GetScore.Death))
                {
                    player4.P4Score += 10;
                    player4.Score = 0;
                }
                else
                {
                    if (player4.Score.HasFlag(GetScore.First))
                    {
                        player4.P4Score += 10;
                    }
                    if (player4.Score.HasFlag(GetScore.Solo))
                    {
                        player4.P4Score += 15;
                    }
                    if (player4.Score.HasFlag(GetScore.Coin))
                    {
                        player4.P4Score += 10;
                    }
                    player4.Score = 0;
                }
            }
            else
            {
                Score = 0;
            }
            pointPlusBool = false;
        }
    }

}
