using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerState;
using DG.Tweening;
/// <summary>
/// ポイント計算してくれます
/// </summary>
public class PointManager : MonoBehaviour
{
    [Header("インスタンスしたいもの")]
    [SerializeField, Tooltip("Goalのゴールした人数を使いたい")] Goal _goal;
    [SerializeField, Tooltip("プレイヤーのリスト")] List<GameObject> _players = new();
    [SerializeField, Tooltip("スライダーのリスト")] List<Slider> _sliders = new();
    [SerializeField, Tooltip("スライダーの変化時間")] float _changeValueInterval;

    [Header("他のところに渡したい物")]
    [Tooltip("ゴールに渡すbool")] public bool _isCheck;
    void Update()
    {
        if (_isCheck)
        {
            ChangePlayerScore();
            //点数計算後にプレイヤーがわかるようにスライダーを動かす
            for (int i = 0; i < _players.Count; i++)
            {
                ChangeSliderValue(_sliders[i], _players[i].GetComponent<PlayerMove>()._scorePoint);
            }
            //ゴールプレイヤーのリストのクリア
            _goal.GoalPlayers.Clear();
            _isCheck = false;
        }

    }
    /// <summary>
    /// 点数計算
    /// </summary>
    void ChangePlayerScore()
    {
        //全員ゴールしてなかったら
        if (_goal.GoalPlayers.Count != Menu._playerNumber)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                //ゴールしたかどうか判定
                //していなかった場合点数なし
                if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.isGoal))
                {
                    //死んでいるか判定
                    //死んでいた場合点数は少ししかもらえない
                    if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Death))
                    {
                        _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        Debug.Log(_players[i].name + "のScore状態は" + _players[i].GetComponent<PlayerMove>().Score);
                        _players[i].GetComponent<PlayerMove>().Score = 0;
                    }
                    else
                    {
                        //ゴールした時のポイント デス判定があるからここに書いた
                        _players[i].GetComponent<PlayerMove>()._scorePoint += 20;
                        //一位判定
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.First))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        }
                        //一人だけゴール判定
                        else if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Solo))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 20;
                        }
                        //コイン判定
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Coin))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 15;
                        }
                        Debug.Log(_players[i].name + "のScore状態は" + _players[i].GetComponent<PlayerMove>().Score);
                        _players[i].GetComponent<PlayerMove>().Score = 0;
                    }
                }
                else
                {
                    Debug.Log(_players[i].name + "のScore状態は" + _players[i].GetComponent<PlayerMove>().Score);
                    _players[i].GetComponent<PlayerMove>().Score = 0;
                }
                Debug.Log(_players[i].name + "のポイントは" + _players[i].GetComponent<PlayerMove>()._scorePoint);
            }
        }
    }
    /// <summary>
    /// スライダーを動かす
    /// </summary>
    /// <param name="_slider"></param>
    /// <param name="value"></param>
    void ChangeSliderValue(Slider _slider, float value)
    {
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
    }
}
