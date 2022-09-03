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
    private GameObject randomObj;
    public GameObject[] itemPrefabs;
    private int _random;
    private int choiceNum;
    [SerializeField] List<GameObject> SummonPositionList;

    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
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
        //////使ったアイテム戻す！
    }
    void ChoseItem()
    {
        //myListの中からランダムで1つを選ぶ
        randomObj = myList[Random.Range(0, myList.Count-1)];
        ////選んだオブジェクトをuseListに追加
        useList.Add(randomObj);
        //randomObj.layer = LayerMask.NameToLayer("Mejirushi");
        ////選んだオブジェクトのリスト番号を取得
        choiceNum = myList.IndexOf(randomObj);
        ////同じリスト番号をmyListから削除
        myList.RemoveAt(choiceNum);
        Debug.Log("選ぶよ！まだできてない");
        _isChoiceItem = false;
    }
    /// <summary>
    /// アイテムをランダムに自分のところに召喚する。
    /// </summary>
    /// <param name="spawnPos">自分の場所</param>
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
