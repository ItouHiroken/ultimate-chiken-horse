using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerState;
using DG.Tweening;

public class PointManager : MonoBehaviour
{
    [SerializeField] GameManager gameManager;

    [SerializeField] List<GameObject> _players = new();
    [SerializeField] List<Slider> _sliders = new();

    public bool _isCheck;
    [SerializeField] float _changeValueInterval;
    void Update()
    {
        if (gameManager.NowTurn == GameManager.Turn.Result)
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
        if (gameManager.NowTurn == GameManager.Turn.Result)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                Debug.Log("�ڂ����܂���Ƃ��Ă񂯂����񂵂܂�");
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
            }
        }
    }
    void ChangeSliderValue(Slider _slider, float value)
    {
        // DOTween.To() ���g���ĘA���I�ɕω�������
        DOTween.To(() => _slider.value, // �A���I�ɕω�������Ώۂ̒l
            x => _slider.value = x, // �ω��������l x ���ǂ��������邩������
            value, // x ���ǂ̒l�܂ŕω������邩�w������
            _changeValueInterval);   // ���b�����ĕω������邩�w������
    }
}
