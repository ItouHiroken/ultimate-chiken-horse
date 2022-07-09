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
    public GameObject[] itemPrefabs;
    private int _random;
    private int choiceNum;
    [SerializeField] List<GameObject> SummonPositionList;

    public bool _isChoiceItem;
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
    private void Update()
    {
        if (_isChoiceItem == true)
        {
            SpawnRandomItem();
        }
    }
    /// <summary>
    /// �A�C�e���������_���Ɏ����̂Ƃ���ɏ�������B
    /// </summary>
    /// <param name="spawnPos">�����̏ꏊ</param>
    void SpawnRandomItem()
    {
        Vector2 spawnPos = this.gameObject.transform.position;
        int N = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[N], spawnPos, itemPrefabs[N].transform.rotation);
    }
}
