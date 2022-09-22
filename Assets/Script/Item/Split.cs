using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Split : ItemBase
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove._deBuff = PlayerState.DeBuff.Split;
            Debug.Log(collision.gameObject.name + "をスプリットにしたよ"+playerMove._deBuff);

        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMove playerMove))
        {
            playerMove._deBuff = PlayerState.DeBuff.Default;
            Debug.Log(collision.gameObject.name + "をスプリットからもとに戻したよ");
        }
    }
}
