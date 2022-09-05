using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] GameObject P3;
    [SerializeField] GameObject P4;

    [SerializeField] float P1Point = 0;
    [SerializeField] float P2Point = 0;
    [SerializeField] float P3Point = 0;
    [SerializeField] float P4Point = 0;

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
            ChangeValue(P1Slider,P1.GetComponent<Player1Move>().P1Score);
            ChangeValue(P2Slider,P2.GetComponent<Player2Move>().P2Score);
            //ChangeValue(P3Slider,P3.GetComponent<Player3Move>().P3Score);
            //ChangeValue(P4Slider,P4.GetComponent<Player4Move>().P4Score);
        }
    }
    void ChangeValue(Slider _slider, float value)
    {
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
    }
}
