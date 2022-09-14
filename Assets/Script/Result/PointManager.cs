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
                ChangeSliderValue(P1Slider, P1.GetComponent<PlayerMove>()._scorePoint);
                ChangeSliderValue(P2Slider, P2.GetComponent<PlayerMove>()._scorePoint);
                ChangeSliderValue(P3Slider, P3.GetComponent<PlayerMove>()._scorePoint);
                ChangeSliderValue(P4Slider, P4.GetComponent<PlayerMove>()._scorePoint);
                _isCheck = false;
            }
        }
    }
    void ChangePlayerScore()
    {
        if (gameManager.NowTurn == GameManager.Turn.Result)
        {
            Debug.Log("ぼくいまからとくてんけいさんします");
            if (P1.GetComponent<PlayerMove>().Score.HasFlag(GetScore.isGoal))
            {
                if (P1.GetComponent<PlayerMove>().Score.HasFlag(GetScore.Death))
                {
                    P1.GetComponent<PlayerMove>()._scorePoint += 10;
                    P1.GetComponent<PlayerMove>().Score = 0;
                }
                else
                {
                    if (P1.GetComponent<PlayerMove>().Score.HasFlag(GetScore.First))
                    {
                        P1.GetComponent<PlayerMove>()._scorePoint += 10;
                    }
                    if (P1.GetComponent<PlayerMove>().Score.HasFlag(GetScore.Solo))
                    {
                        P1.GetComponent<PlayerMove>()._scorePoint += 15;
                    }
                    if (P1.GetComponent<PlayerMove>().Score.HasFlag(GetScore.Coin))
                    {
                        P1.GetComponent<PlayerMove>()._scorePoint += 10;
                    }
                    P1.GetComponent<PlayerMove>().Score = 0;
                }
            }
            else
            {
                P1.GetComponent<PlayerMove>().Score = 0;
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
