using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NowPeople : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nowPeople;
    void Update()
    {
        nowPeople.text = string.Format("Number of Player:{0}", Menu._playerNumber);
    }
}
