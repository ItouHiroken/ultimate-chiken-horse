using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using PlayerState;
using DG.Tweening;
/// <summary>
/// �|�C���g�v�Z���Ă���܂�
/// </summary>
public class PointManager : MonoBehaviour
{
    [Header("�C���X�^���X����������")]
    [SerializeField, Tooltip("Goal�̃S�[�������l�����g������")] Goal _goal;
    [SerializeField, Tooltip("�v���C���[�̃��X�g")] List<PlayerMove> _players = new();
    [SerializeField, Tooltip("�X���C�_�[�̃��X�g")] List<Slider> _sliders = new();
    [SerializeField, Tooltip("�X���C�_�[�̕ω�����")] float _changeValueInterval;

    [Header("���̂Ƃ���ɓn��������")]
    [Tooltip("�S�[���ɓn��bool")] public bool _isCheck;
    void Update()
    {
        if (_isCheck)
        {
            ChangePlayerScore();
            //�_���v�Z��Ƀv���C���[���킩��悤�ɃX���C�_�[�𓮂���
            for (int i = 0; i < _players.Count; i++)
            {
                ChangeSliderValue(_sliders[i], _players[i]._scorePoint);
            }
            //�S�[���v���C���[�̃��X�g�̃N���A
            _goal.GoalPlayers.Clear();
            _isCheck = false;
        }

    }
    /// <summary>
    /// �_���v�Z
    /// </summary>
    void ChangePlayerScore()
    {
        //�S���S�[�����ĂȂ�������
        if (_goal.GoalPlayers.Count != Menu._playerNumber)
        {
            for (int i = 0; i < _players.Count; i++)
            {
                //�S�[���������ǂ�������
                //���Ă��Ȃ������ꍇ�_���Ȃ�
                if (_players[i].Score.HasFlag(GetScore.isGoal))
                {
                    //����ł��邩����
                    //����ł����ꍇ�_���͏����������炦�Ȃ�
                    if (_players[i].Score.HasFlag(GetScore.Death))
                    {
                        _players[i]._scorePoint += 10;
                        Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].Score);
                        _players[i].Score = 0;
                    }
                    else
                    {
                        //�S�[���������̃|�C���g �f�X���肪���邩�炱���ɏ�����
                        _players[i]._scorePoint += 20;
                        //��ʔ���
                        if (_players[i].Score.HasFlag(GetScore.First))
                        {
                            _players[i]._scorePoint += 10;
                        }
                        //��l�����S�[������
                        else if (_players[i].Score.HasFlag(GetScore.Solo))
                        {
                            _players[i]._scorePoint += 20;
                        }
                        //�R�C������
                        if (_players[i].Score.HasFlag(GetScore.Coin))
                        {
                            _players[i]._scorePoint += 15;
                        }
                        Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].Score);
                        _players[i].Score = 0;
                    }
                }
                else
                {
                    Debug.Log(_players[i].name + "��Score��Ԃ�" + _players[i].Score);
                    _players[i].GetComponent<PlayerMove>().Score = 0;
                }
                Debug.Log(_players[i].name + "�̃|�C���g��" + _players[i]._scorePoint);
            }
        }
    }
    /// <summary>
    /// �X���C�_�[�𓮂���
    /// </summary>
    /// <param name="_slider"></param>
    /// <param name="value"></param>
    void ChangeSliderValue(Slider _slider, float value)
    {
        // DOTween.To() ���g���ĘA���I�ɕω�������
        DOTween.To(() => _slider.value, // �A���I�ɕω�������Ώۂ̒l
            x => _slider.value = x, // �ω��������l x ���ǂ��������邩������
            value, // x ���ǂ̒l�܂ŕω������邩�w������
            _changeValueInterval);   // ���b�����ĕω������邩�w������
    }
}
