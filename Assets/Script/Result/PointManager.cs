using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerState;
using DG.Tweening;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameManager _gameManager;
    [SerializeField] Goal _goal;

    [SerializeField] List<GameObject> _players = new();
    [SerializeField] List<Slider> _sliders = new();

    public bool _isCheck;
    [SerializeField] float _changeValueInterval;
    void Update()
    {
        if (_gameManager.NowTurn == GameManager.Turn.Result)
        {
            if (_isCheck)
            {
                ChangePlayerScore();
                for (int i = 0; i < _players.Count; i++)
                {
                    ChangeSliderValue(_sliders[i], _players[i].GetComponent<PlayerMove>()._scorePoint);
                }
                _isCheck = false;
            }
        }
    }
    void ChangePlayerScore()
    {
        if (_gameManager.NowTurn == GameManager.Turn.Result && _goal.goalPlayers.Count != Menu._playerNumber)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.isGoal))
                {
                    if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Death))
                    {
                        _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        _players[i].GetComponent<PlayerMove>().Score = 0;
                    }
                    else
                    {
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.First))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        }
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Solo))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 15;
                        }
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Coin))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        }
                        _players[i].GetComponent<PlayerMove>().Score = 0;
                    }
                }
                else
                {
                    _players[i].GetComponent<PlayerMove>().Score = 0;
                }
                Debug.Log(_players[i].name+"のポイントは"+_players[i].GetComponent<PlayerMove>()._scorePoint);
            }
        }
    }
    void ChangeSliderValue(Slider _slider, float value)
    {
        // DOTween.To() を使って連続的に変化させる
        DOTween.To(() => _slider.value, // 連続的に変化させる対象の値
            x => _slider.value = x, // 変化させた値 x をどう処理するかを書く
            value, // x をどの値まで変化させるか指示する
            _changeValueInterval);   // 何秒かけて変化させるか指示する
    }
}
