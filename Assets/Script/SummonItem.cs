using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        //myListの中からランダムで1つを選ぶ
        randomObj = myList[Random.Range(0, myList.Count)];
        ////選んだオブジェクトをuseListに追加
        //useList.Add(randomObj);
        ////randomObj.layer = LayerMask.NameToLayer("Mejirushi");
        ////選んだオブジェクトのリスト番号を取得
        //choiceNum = myList.IndexOf(randomObj);
        ////同じリスト番号をmyListから削除
        //myList.RemoveAt(choiceNum);
    }
}
