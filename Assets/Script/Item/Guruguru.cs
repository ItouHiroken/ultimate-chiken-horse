using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���܂�
/// </summary>
public class Guruguru : ItemBase
{
    [SerializeField]float _kaitenSpeed;
    protected new void Update()
    {
        if(base._nowTurn==GameManager.Turn.GamePlay)
        {
            Mawaru();
        } 
        base.Update();
    }
    void Mawaru()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.back);// ���݂̎��M�̉�]�̏����擾����B
        Quaternion q = gameObject.transform.rotation;// �������āA���g�ɐݒ�
        gameObject.transform.rotation = q * rot;
    }
}
