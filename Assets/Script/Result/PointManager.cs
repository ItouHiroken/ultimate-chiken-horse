using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerState;
using DG.Tweening;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] GameObject P3;
    [SerializeField] GameObject P4;

    [SerializeField] Slider P1Slider;
    [SerializeField] Slider P2Slider;
    [SerializeField] Slider P3Slider;
    [SerializeField] Slider P4Slider;

    public bool _isCheck;

    [SerializeField] float _changeValueInterval;
    void Update()
    {
        if (gameManager.NowTurn == GameManager.Turn.Result)
        {
            if (_isCheck)
            {
                ChangePlayerScore();
                ChangeSliderValue(P1Slider, P1.GetComponent<Player1Move>().P1Score);
                ChangeSliderValue(P2Slider, P2.GetComponent<Player2Move>().P2Score);
                ChangeSliderValue(P3Slider, P3.GetComponent<Player3Move>().P3Score);
                ChangeSliderValue(P4Slider, P4.GetComponent<Player4Move>().P4Score);
                _isCheck = false;
            }
        }
    }
    void ChangePlayerScore()
    {
        if (gameManager.NowTurn == GameManager.Turn.Result)
        {
            Debug.Log("ぼくいまからとくてんけいさんします");
            if (P1.GetComponent<Player1Move>().Score.HasFlag(GetScore.isGoal))
            {
                if (P1.GetComponent<Player1Move>().Score.HasFlag(GetScore.Death))
                {
                    P1.GetComponent<Player1Move>().P1Score += 10;
                    P1.GetComponent<Player1Move>().Score = 0;
                }
                else
                {
                    if (P1.GetComponent<Player1Move>().Score.HasFlag(GetScore.First))
                    {
                        P1.GetComponent<Player1Move>().P1Score += 10;
                    }
                    if (P1.GetComponent<Player1Move>().Score.HasFlag(GetScore.Solo))
                    {
                        P1.GetComponent<Player1Move>().P1Score += 15;
                    }
                    if (P1.GetComponent<Player1Move>().Score.HasFlag(GetScore.Coin))
                    {
                        P1.GetComponent<Player1Move>().P1Score += 10;
                    }
                    P1.GetComponent<Player1Move>().Score = 0;
                }
            }
            else
            {
                P1.GetComponent<Player1Move>().Score = 0;
            }
            if (P2.GetComponent<Player2Move>().Score.HasFlag(GetScore.isGoal))
            {
                if (P2.GetComponent<Player2Move>().Score.HasFlag(GetScore.Death))
                {
                    P2.GetComponent<Player2Move>().P2Score += 10;
                    P2.GetComponent<Player2Move>().Score = 0;
                }
                else
                {
                    if (P2.GetComponent<Player2Move>().Score.HasFlag(GetScore.First))
                    {
                        P2.GetComponent<Player2Move>().P2Score += 10;
                    }
                    if (P2.GetComponent<Player2Move>().Score.HasFlag(GetScore.Solo))
                    {
                        P2.GetComponent<Player2Move>().P2Score += 15;
                    }
                    if (P2.GetComponent<Player2Move>().Score.HasFlag(GetScore.Coin))
                    {
                        P2.GetComponent<Player2Move>().P2Score += 10;
                    }
                    P2.GetComponent<Player2Move>().Score = 0;
                }
            }
            else
            {
                P2.GetComponent<Player2Move>().Score = 0;
            }
            if (P3.GetComponent<Player3Move>().Score.HasFlag(GetScore.isGoal))
            {
                if (P3.GetComponent<Player3Move>().Score.HasFlag(GetScore.Death))
                {
                    P3.GetComponent<Player3Move>().P3Score += 10;
                    P3.GetComponent<Player3Move>().Score = 0;
                }
                else
                {
                    if (P3.GetComponent<Player3Move>().Score.HasFlag(GetScore.First))
                    {
                        P3.GetComponent<Player3Move>().P3Score += 10;
                    }
                    if (P3.GetComponent<Player3Move>().Score.HasFlag(GetScore.Solo))
                    {
                        P3.GetComponent<Player3Move>().P3Score += 15;
                    }
                    if (P3.GetComponent<Player3Move>().Score.HasFlag(GetScore.Coin))
                    {
                        P3.GetComponent<Player3Move>().P3Score += 10;
                    }
                    P3.GetComponent<Player3Move>().Score = 0;
                }
            }
            else
            {
                P3.GetComponent<Player3Move>().Score = 0;
            }
            if (P4.GetComponent<Player4Move>().Score.HasFlag(GetScore.isGoal))
            {
                if (P4.GetComponent<Player4Move>().Score.HasFlag(GetScore.Death))
                {
                    P4.GetComponent<Player4Move>().P4Score += 10;
                    P4.GetComponent<Player4Move>().Score = 0;
                }
                else
                {
                    if (P4.GetComponent<Player4Move>().Score.HasFlag(GetScore.First))
                    {
                        P4.GetComponent<Player4Move>().P4Score += 10;
                    }
                    if (P4.GetComponent<Player4Move>().Score.HasFlag(GetScore.Solo))
                    {
                        P4.GetComponent<Player4Move>().P4Score += 15;
                    }
                    if (P4.GetComponent<Player4Move>().Score.HasFlag(GetScore.Coin))
                    {
                        P4.GetComponent<Player4Move>().P4Score += 10;
                    }
                    P4.GetComponent<Player4Move>().Score = 0;
                }
            }
            else
            {
                P4.GetComponent<Player4Move>().Score = 0;
            }
        }
    }
    void ChangeSliderValue(Slider _slider, float value)
    {
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
    }
}
