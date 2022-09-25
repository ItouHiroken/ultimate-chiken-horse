using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 全部のアイテムにつけるもの
/// </summary>
public class DestroyItem : MonoBehaviour
{
    private GameManager.Turn Turn;
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").gameObject;
    }
    private void Update()
    {
        Turn = _gameManager.GetComponent<GameManager>().NowTurn;
        if (Turn == GameManager.Turn.SetItem&&gameObject.tag!="isChoice")
        {
            Destroy(gameObject);
        }
    }
}