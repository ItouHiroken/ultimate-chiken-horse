using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// プレイヤーの移動に関するもの
/// </summary>
public class Player1Move : PlayerBase
{
    [Tooltip("走れるかどうかチェック")] bool _dashCheck;
    [SerializeField] private float _horizonSpeedLimiter;
    [SerializeField] private float _jumpSpeedLimiter;
    protected override void SpeedController()
    {
        _horizonSpeedLimiter = WalkSpeedLimiter;
        _jumpSpeedLimiter = 30f;
    }

    protected new void Update()/////←←←←←←←←←←←これnewつけるとなにかを非表示にするらしい、なにがなんなのかわかんないから聞く
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            _dashCheck = !_dashCheck;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {

            if (JumpChecker == 1 && GroundCheck)
            {
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
            }
            if (JumpChecker == 1 && !GroundCheck && RightWallCheck)
            {
                Rb.AddForce(Vector2.up * JumpPower, ForceMode2D.Impulse);
                Rb.AddForce(Vector2.right * 40, ForceMode2D.Impulse);

            }
            if (JumpChecker == 1 && !GroundCheck && LeftWallCheck)
            {
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
            if (_dashCheck == true)
            {
                _horizonSpeedLimiter = RunSpeedLimiter;
            }
        }
        //左入力で左向きに動く
        else if (horizontalKey < 0)
        {
            Rb.AddForce(Vector2.left * Speed, ForceMode2D.Impulse);
            if (_dashCheck == true)
            {
                _horizonSpeedLimiter = RunSpeedLimiter;
            }
        }
        if (_dashCheck == false)
        {
            _horizonSpeedLimiter = WalkSpeedLimiter;
        }
        if (Rb.velocity.y < -_jumpSpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, -_jumpSpeedLimiter);
        }

        if (RightWallCheck || LeftWallCheck)
        {
            if (Rb.velocity.y < -_jumpSpeedLimiter / 2)
            {
                Rb.velocity = new Vector2(Rb.velocity.x, -_jumpSpeedLimiter / 2);
            }
        }
        if (Rb.velocity.y > _jumpSpeedLimiter)
        {
            Rb.velocity = new Vector2(Rb.velocity.x, _jumpSpeedLimiter);
        }
        if (Rb.velocity.x > _horizonSpeedLimiter)
        {
            Rb.velocity = new Vector2(_horizonSpeedLimiter, Rb.velocity.y);
        }
        if (Rb.velocity.x < -_horizonSpeedLimiter)
        {
            Rb.velocity = new Vector2(-_horizonSpeedLimiter, Rb.velocity.y);
        }
    }
}

