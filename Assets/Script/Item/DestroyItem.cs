using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyItem : MonoBehaviour
{
    public GameManager.Turn Turn;
    [SerializeField, Tooltip("�Q�[���}�l�[�W���[����Q�Ƃ�����")] GameObject _gameManager;
    private void Update()
    {
        Turn = _gameManager.GetComponent<GameManager>().NowTurn;
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (Turn==GameManager.Turn.SetItem&&Input.GetKeyDown(KeyCode.Space))
        {
            collision.gameObject.SetActive(false);
            Debug.Log("�I���{�^������������ɂ���i�������̒��Ɂj");
        }
    }
}