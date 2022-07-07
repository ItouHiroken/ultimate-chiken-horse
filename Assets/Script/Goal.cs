using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    [SerializeField] Player1Move controller1;
    //[SerializeField] Player2Move controller2;
    //[SerializeField] Player3Move controller3;
    //[SerializeField] Player4Move controller4;
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (other.name == "Player1")
            {
                controller1.enabled = false;
            }
        }
    }
}
