using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Q�[���}�l�[�W���[����̎w���ŃJ�[�\�����ʒu�ɂ�����
/// </summary>
public class CursorStart : MonoBehaviour
{
    [SerializeField, Tooltip("�J�[�\������")] List<GameObject> _cursors = new();
    [SerializeField, Tooltip("�J�[�\���̒�ʒu")] List<GameObject> _position = new();
    public bool SelectSceneStart;
    private void Update()
    {
        if (SelectSceneStart == true)
        {
            for (int i = 0; i < Menu._playerNumber; i++)
            {
                _cursors[i].transform.position = _position[i].transform.position;//�J�[�\�����ʒu�Ɉړ�������
                _cursors[i].GetComponent<PlayerCursor>().enabled = true;//�J�[�\���������悤�ɂ���
            }
            SelectSceneStart = false;
        }
    }
}
