using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���I����ʂ̃A�C�e����������ЂƁB
/// </summary>
public class SummonItem : MonoBehaviour
{
    [SerializeField] List<GameObject> myList;
    [SerializeField] List<GameObject> itemList;
    public List<GameObject> useList = new List<GameObject>();
    private GameObject randomObj;
    private int choiceNum;
    [SerializeField] List<GameObject> SummonPositionList;

    //[SerializeField] GameObject SummonPosition1;
    //[SerializeField] GameObject SummonPosition2;
    //[SerializeField] GameObject SummonPosition3;
    private void Start()
    {
        //myList�̒����烉���_����1��I��
        randomObj = myList[Random.Range(0, myList.Count)];
        ////�I�񂾃I�u�W�F�N�g��useList�ɒǉ�
        //useList.Add(randomObj);
        ////randomObj.layer = LayerMask.NameToLayer("Mejirushi");
        ////�I�񂾃I�u�W�F�N�g�̃��X�g�ԍ����擾
        //choiceNum = myList.IndexOf(randomObj);
        ////�������X�g�ԍ���myList����폜
        //myList.RemoveAt(choiceNum);
    }
}
