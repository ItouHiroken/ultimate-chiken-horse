using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �Q�[���}�l�[�W���[����̎w���Ńv���C���[�̏����ƋN��
/// </summary>
public class StartingPoint : MonoBehaviour
{
    [SerializeField] public List<GameObject> _players = new();
    [SerializeField] List<GameObject> _position = new();
    public bool PlaySceneStart;
    private void Update()
    {
        if (PlaySceneStart == true)
        {
            for (int i = 0; i < Menu._playerNumber; i++)
            {
                _players[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//���x�����0�ɂ���
                _players[i].transform.position = _position[i].transform.position;//�v���C���[���ʒu�ɒu��
                _players[i].GetComponent<PlayerMove>().enabled = true;//�v���C���[��������悤�ɂ���
            }
            PlaySceneStart = false;
        }
    }
}