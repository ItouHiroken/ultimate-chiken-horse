using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーの移動に関するもの
/// </summary>
public class Player1Move : PlayerBase
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (JumpChecker == 1 && GroundCheck)
            {
                Debug.Log("a");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
            if (JumpChecker == 1 && !GroundCheck && WallCheck && MyPosition > StartPosition)
            {
                Debug.Log("ue");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);

            }
            if (JumpChecker == 1 && !GroundCheck && WallCheck && MyPosition < StartPosition)
            {
                Debug.Log("shita");
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.left * 40, ForceMode2D.Impulse);
            }
        }
    }
    private void FixedUpdate()
    {
        float horizontalKey = Input.GetAxis("Horizontal");

        //右入力で左向きに動く
        if (horizontalKey > 0)
        {
            Rb.AddForce(Vector2.right * Speed, ForceMode2D.Impulse);
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            Rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
        }
        if (Rb.velocity.y < -SpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -SpeedLimiter);
        }

        if (WallCheck)
        {
            if (Rb.velocity.y < -SpeedLimiter / 2)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, -SpeedLimiter / 2);
            }
        }
        if (Rb.velocity.y > SpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, SpeedLimiter);
        }
        if (Rb.velocity.x > SpeedLimiter)
        {
            Rb.velocity = new Vector2(SpeedLimiter, Rb.velocity.y);
        }
        if (Rb.velocity.x < -SpeedLimiter)
        {
            Rb.velocity = new Vector2(-SpeedLimiter, Rb.velocity.y);
        }
    }

}

