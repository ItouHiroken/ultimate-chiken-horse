using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : ItemBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove playerMove))
        {

            playerMove._deBuff = PlayerState.DeBuff.Slow;
            Debug.Log(collision.gameObject.name + "をスロウにしたよ");
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove._deBuff = PlayerState.DeBuff.Default;
            Debug.Log(collision.gameObject.name + "をスロウからもとに戻したよ");
        }
    }
}
