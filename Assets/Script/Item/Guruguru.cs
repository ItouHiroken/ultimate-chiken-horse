using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���܂�
/// </summary>
public class Guruguru : ItemBase
{
    GameObject _manager;
    GameManager.Turn _turn;
    [SerializeField]float _kaitenSpeed;
    private void Start()
    {
        _manager = GameObject.Find("GameManager").gameObject;
    }
    protected new void Update()
    {
        TurnChecker(_manager);
        if(_turn==GameManager.Turn.GamePlay)
        {
            Mawaru();
        } 
    }
    void Mawaru()
    {
        Quaternion rot = Quaternion.AngleAxis(_kaitenSpeed, Vector3.back);
        // ���݂̎��M�̉�]�̏����擾����B
        Quaternion q = gameObject.transform.rotation;
        // �������āA���g�ɐݒ�
        gameObject.transform.rotation = q * rot;
    }
    void TurnChecker(GameObject a)
    {
        _turn = a.GetComponent<GameManager>().NowTurn;
    }
}
