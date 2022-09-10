using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("ゲームマネージャーから参照したい")] GameObject _gameManager;
    public bool _isSelect=false;
    private void Update()
    {
        Turn = _gameManager.GetComponent<GameManager>().NowTurn;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Turn==GameManager.Turn.SetItem&&!_isSelect)
        {
            Destroy(gameObject);
        }
    }
}