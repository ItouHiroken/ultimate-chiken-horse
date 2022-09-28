using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// �A�C�e���I����ʂ̃A�C�e����������ЂƁB
/// </summary>
public class SummonItem : MonoBehaviour
{
    [Header("�C���X�^���X����������")]
    [SerializeField, Tooltip("������A�C�e���������ɓ����A�A�C�e�����Z�b�g����Ƃ��ɃR�s�[����p")] List<GameObject> itemPrefabs;
    [SerializeField, Tooltip("�����ꏊ")] List<GameObject> SummonPositionList;
    
    [Header("����������")]
    [SerializeField, Tooltip("�����A�C�e�����X�g")]  List<GameObject> useList = new List<GameObject>();
    [SerializeField, Tooltip("�A�C�e�������p���X�g")] List<GameObject> myList;
   
    [Header("���̂Ƃ���ɓn������")]
    [Tooltip("�Q�[���}�l�[�W���[�̎w��")] public bool _isChoiceItem;
    private void Update()
    {
        if (_isChoiceItem)
        {
            foreach (var i in SummonPositionList)
            {
                ItemReset();
                ChoseItem();
                SpawnRandomItem(i);
            }
        }
        //////�g�����A�C�e���߂��I
        _isChoiceItem = false;
    }

    /// <summary>
    /// �A�C�e�����X�g���Z�b�g����B
    /// </summary>
    void ItemReset()
    {
        myList.Clear();
        myList = new List<GameObject>(itemPrefabs);
    }
    void ChoseItem()
    {
        //myList�̒����烉���_����1��I��
        GameObject randomObj = myList[Random.Range(0, myList.Count)];
        ////�I�񂾃I�u�W�F�N�g��useList�ɒǉ�
        useList.Add(randomObj);
        ////�I�񂾃I�u�W�F�N�g�̃��X�g�ԍ����擾
        int choiceNum = myList.IndexOf(randomObj);
        ////�������X�g�ԍ���myList����폜
        myList.RemoveAt(choiceNum);
        _isChoiceItem = false;
    }
    /// <summary>
    /// �A�C�e���������_���Ɏ����̂Ƃ���ɏ�������B
    /// </summary>
    /// <param name="spawnPos">�����̏ꏊ</param>
    void SpawnRandomItem(GameObject SummonPoint)
    {
        Vector2 spawnPos = SummonPoint.gameObject.transform.position;
        int N = Random.Range(0, itemPrefabs.Count);
        Instantiate(itemPrefabs[N], spawnPos, itemPrefabs[N].transform.rotation);
    }
}
