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

    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;
    public GameManager.Turn Turn;
    public bool _isChoiceItem;
    private void Update()
    {
        if (_isChoiceItem)
        {
            foreach (var i in SummonPositionList)
            {
                ChoseItem();
                SpawnRandomItem(i);
            }
        }
        //////�g�����A�C�e���߂��I
    }
    void ChoseItem()
    {
        //myList�̒����烉���_����1��I��
        randomObj = myList[Random.Range(0, myList.Count-1)];
        ////�I�񂾃I�u�W�F�N�g��useList�ɒǉ�
        useList.Add(randomObj);
        //randomObj.layer = LayerMask.NameToLayer("Mejirushi");
        ////�I�񂾃I�u�W�F�N�g�̃��X�g�ԍ����擾
        choiceNum = myList.IndexOf(randomObj);
        ////�������X�g�ԍ���myList����폜
        myList.RemoveAt(choiceNum);
        Debug.Log("�I�Ԃ�I�܂��ł��ĂȂ�");
        _isChoiceItem = false;
    }
    /// <summary>
    /// �A�C�e���������_���Ɏ����̂Ƃ���ɏ�������B
    /// </summary>
    /// <param name="spawnPos">�����̏ꏊ</param>
    void SpawnRandomItem(GameObject SummonPoint)
    {
        Vector2 spawnPos = SummonPoint.gameObject.transform.position;
        int N = Random.Range(0, itemPrefabs.Length);
        Instantiate(itemPrefabs[N], spawnPos, itemPrefabs[N].transform.rotation);
        _isChoiceItem = false;
    }
    public void TurnChecker(GameObject a)
    {
        Turn = a.GetComponent<GameManager>().NowTurn;
    }
}
