using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ��]����p�x�������ŃC���X�y�N�^�[�ɓ���Ďg�����I
/// </summary>
public class ItemKaiten : MonoBehaviour
{
    [SerializeField]int kaitenIndex;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            Quaternion rot = Quaternion.AngleAxis(kaitenIndex, Vector3.forward);
            // ���݂̎��M�̉�]�̏����擾����B
            Quaternion q = this.transform.rotation;
            // �������āA���g�ɐݒ�
            this.transform.rotation = q * rot;
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            Quaternion rot = Quaternion.AngleAxis(kaitenIndex, Vector3.back);
            // ���݂̎��M�̉�]�̏����擾����B
            Quaternion q = this.transform.rotation;
            // �������āA���g�ɐݒ�
            this.transform.rotation = q * rot;
        }
    }
}
