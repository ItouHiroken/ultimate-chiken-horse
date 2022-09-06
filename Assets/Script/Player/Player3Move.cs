using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayerState;

public class Player3Move : PlayerBase
{
    [SerializeField][Tooltip("体力")] private int _hp = default;
    [Tooltip("走れるかどうかチェック")] bool _dashCheck;
    [SerializeField] private float _horizonSpeedLimiter;
    [SerializeField] private float _jumpSpeedLimiter;
    [SerializeField][Tooltip("自分の動きonoffするため")] Player3Move controller;
    public PlayerState.GetScore Score;
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;

    [SerializeField] public int P3Score;

    protected override void SpeedController()
    {
        _horizonSpeedLimiter = WalkSpeedLimiter;
        _jumpSpeedLimiter = 50f;
    }

    protected new void Update()/////←←←←←←←←←←←これnewつけるとなにかを非表示にするらしい、なにがなんなのかわかんないから聞く
    {
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.GamePlay)
        {
            base.Update();
            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                _dashCheck = !_dashCheck;
            }
            bool jump = Input.GetButtonDown("P3Jump");
            if (jump == true)
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
        if (Turn == GameManager.Turn.Result)
        {
            if (Score.HasFlag(GetScore.isGoal))
            {
                if (Score.HasFlag(GetScore.Death))
                {
                    P3Score += 10;
                    Score = 0;
                }
                else
                {
                    if (Score.HasFlag(GetScore.First))
                    {
                        P3Score += 10;
                    }
                    if (Score.HasFlag(GetScore.Solo))
                    {
                        P3Score += 15;
                    }
                    if (Score.HasFlag(GetScore.Coin))
                    {
                        P3Score += 10;
                    }
                    Score = 0;
                }
            }
            else
            {
                Score = 0;
            }
        }


    }
    private void FixedUpdate()
    {
        TurnChecker(_gameManager);
        if (Turn == GameManager.Turn.GamePlay)
        {
            float horizontalKey = Input.GetAxis("P3Horizontal");

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
    /// <summary>
    /// ダメージ受ける用
    /// </summary>
    /// <param name="collision"></param>
    protected void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out DamageController damage))
        {
            _hp -= damage.Damage;
            if (_hp <= 0)
            {
                controller.enabled = false;
                Score |= PlayerState.GetScore.Death;
                Score &= ~PlayerState.GetScore.Default;
                Debug.Log(Score);
            }
        }
        if (collision.gameObject.CompareTag("Coin"))
        {
            collision.gameObject.tag = "isUsed";
            Score |= PlayerState.GetScore.Coin;
            Debug.Log(Score);
        }
        if (collision.gameObject.name == "Goal")
        {
            Score |= PlayerState.GetScore.isGoal;
            //collision.gameObject.SetActive(false);
            Debug.Log(Score);
        }
    }
    void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}
