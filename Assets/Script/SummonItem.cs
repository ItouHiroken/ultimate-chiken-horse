using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// アイテム選択画面のアイテム生成するひと。
/// </summary>
public class SummonItem : MonoBehaviour
{
    [SerializeField] List<GameObject> myList;
    [SerializeField] List<GameObject> itemList;
    public List<GameObject> useList = new List<GameObject>();
    [SerializeField]List<GameObject> itemPrefabs;
    [SerializeField] List<GameObject> SummonPositionList;

    public bool _isChoiceItem;
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
        //////使ったアイテム戻す！
        _isChoiceItem = false;
    }

    /// <summary>
    /// アイテムリストをセットする。
    /// </summary>
    void ItemReset()
    {
        myList.Clear();
        myList = new List<GameObject>(itemPrefabs);
    }
    void ChoseItem()
    {
        //myListの中からランダムで1つを選ぶ
        GameObject randomObj = myList[Random.Range(0, myList.Count)];
        ////選んだオブジェクトをuseListに追加
        useList.Add(randomObj);
        ////選んだオブジェクトのリスト番号を取得
        int choiceNum = myList.IndexOf(randomObj);
        ////同じリスト番号をmyListから削除
        myList.RemoveAt(choiceNum);
        _isChoiceItem = false;
    }
    /// <summary>
    /// アイテムをランダムに自分のところに召喚する。
    /// </summary>
    /// <param name="spawnPos">自分の場所</param>
    void SpawnRandomItem(GameObject SummonPoint)
    {
        Vector2 spawnPos = SummonPoint.gameObject.transform.position;
        int N = Random.Range(0, itemPrefabs.Count);
        Instantiate(itemPrefabs[N], spawnPos, itemPrefabs[N].transform.rotation);
    }
}
