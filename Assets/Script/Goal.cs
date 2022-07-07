using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Goal : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (other.name == "Player1")
            {
                Player1Move playerscript;
                GameObject obj = GameObject.Find("Player1");
                playerscript = obj.GetComponent<Player1Move>();
                playerscript.enabled = false;
            }
            //    if (other.name == "Player2")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player2");
            //        playerscript = obj.GetComponent<Player2Move>();
            //        playerscript.enabled = false;
            //    }
            //    if (other.name == "Player3")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player3");
            //        playerscript = obj.GetComponent<Player3Move>();
            //        playerscript.enabled = false;
            //    }
            //    if (other.name == "Player4")
            //    {
            //        Player1Move playerscript;
            //        GameObject obj = GameObject.Find("Player4");
            //        playerscript = obj.GetComponent<Player4Move>();
            //        playerscript.enabled = false;
            //    }
        }
    }
}