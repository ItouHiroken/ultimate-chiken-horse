using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �_���[�W��t���������̂ɂ͂��������
/// </summary>
public class DamageController : MonoBehaviour
{
    [Tooltip("�_���[�W��t�^�I")] private int _damage=1;
    public int Damage { get { return _damage; } }
}
