using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;
    [SerializeField] GameObject P1;
    [SerializeField] GameObject P2;
    [SerializeField] GameObject P3;
    [SerializeField] GameObject P4;

    [SerializeField] float P1Point = 0;
    float P2Point = 0;
    float P3Point = 0;
    float P4Point = 0;

    [SerializeField] Slider P1Slider;
    [SerializeField] Slider P2Slider;
    [SerializeField] Slider P3Slider;
    [SerializeField] Slider P4Slider;

    public bool _isCheck;
    void Update()
    {
        if (gameManager.NowTurn == GameManager.Turn.Result)
        {
            if (P1Slider.value < P1.GetComponent<Player1Move>().P1Score)
            {
                P1Slider.value += 0.1f;
            }
            if (P2Slider.value < P2.GetComponent<Player2Move>().P2Score)
            {
                P1Slider.value += 0.1f;
            }
            //if (P3Slider.value < P3.GetComponent<Player3Move>().P3Score)
            //{
            //    P1Slider.value += 0.1f;
            //}
            //if (P4Slider.value < P4.GetComponent<Player4Move>().P4Score)
            //{
            //    P1Slider.value += 0.1f;
            //}
        }
    }
}
