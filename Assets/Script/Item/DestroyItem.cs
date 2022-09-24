using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
    public bool _isSelect = false;
    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").gameObject;
    }
    private void Update()
    {
        Turn = _gameManager.GetComponent<GameManager>().NowTurn;
        if (Turn == GameManager.Turn.SetItem && !_isSelect&&gameObject.tag!="isChoice")
        {
            Destroy(gameObject);
        }
    }
}