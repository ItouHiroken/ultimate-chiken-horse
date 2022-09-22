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
        if (_goal.goalPlayers.Count != Menu._playerNumber)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.isGoal))
                {
                    if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Death))
                    {
                        _players[i].GetComponent<PlayerMove>()._scorePoint += 10;
                        Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].GetComponent<PlayerMove>().Score);
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
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 20;
                        }
                        if (_players[i].GetComponent<PlayerMove>().Score.HasFlag(GetScore.Coin))
                        {
                            _players[i].GetComponent<PlayerMove>()._scorePoint += 15;
                        }
                        Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].GetComponent<PlayerMove>().Score);
                        _players[i].GetComponent<PlayerMove>()._scorePoint += 20;
                        _players[i].GetComponent<PlayerMove>().Score = 0;
                    }
                }
                else
                {
                    Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].GetComponent<PlayerMove>().Score);
                    _players[i].GetComponent<PlayerMove>().Score = 0;
                }
                Debug.Log(_players[i].name + "�̃|�C���g��" + _players[i].GetComponent<PlayerMove>()._scorePoint);
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
