using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// プレイヤーがゴールに入ったあと起こる事のさまざま
/// メモ
/// 1.プレイヤーの移動能力を奪う
/// 2.プレイヤーが生存しているかどうか判断する
/// 3.プレイヤーが入ってきた順番を覚える
/// </summary>
public class Goal : MonoBehaviour
{
    [Tooltip("ポイントマネージャーに渡すゴール順番リスト")][SerializeField] private List<GameObject> goalPlayers = new List<GameObject>();
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.name == "Player1")
            {
                Player1Move playerscript;
                playerscript = collision.GetComponent<Player1Move>();
                playerscript.enabled = false;
                if (playerscript.enabled == false)
                {
                    goalPlayers.Add(collision.gameObject);
                  //  playerscript.isGoal1 = true;
                }
                ///何かしらで渡す量を決める、これはポイントを管理するスクリプトを作ってから考えよう。
                //if (playerscript.isDead == true)///プレイヤーが入った瞬間、死んでいた場合ポイントが減る
                //{

                //}
                //else///生きてたらより多くのポイントが手に入る
                //{
                    
                //}
                //if(playerscript.)
            }
        }
    }
            void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            if (collision.name == "Player1")
            {
                Player1Move playerscript;
               // GameObject obj = GameObject.Find("Player1");
                playerscript = collision.GetComponent<Player1Move>();
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