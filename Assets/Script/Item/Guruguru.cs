using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
/// <summary>
/// ���܂�
/// </summary>
public class Guruguru : MonoBehaviour
{
    Transform target;   // �ړ��ΏۃI�u�W�F�N�g
    [SerializeField]Transform origin;   // ���]���_
    float prevVal;      // �O��̊p�x

    private void Start()
    {
        target = transform;
    }
    public Tween DoRotateAround(float endValue, float duration)
    {
        prevVal = 0.0f;

        // duration�̎��ԂŒl��0�`endValue�܂ŕύX�����Č��]�������Ă�
        Tween ret = DOTween.To(x => RotateAroundPrc(x), 0.0f, endValue, duration);

        return ret;
    }

    /// <summary>
    /// ���]����
    /// </summary>
    /// <param name="value"></param>
    private void RotateAroundPrc(float value)
    {
        // �O��Ƃ̍������v�Z
        float delta = value - prevVal;

        // Y������Ɍ��]�^��
        target.RotateAround(origin.position, Vector3.up, delta);

        // �O��̊p�x���X�V
        prevVal = value;
    }
}
