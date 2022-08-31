using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Menu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField]public static int _playerNumber=4;
    static int _playerName;
    [SerializeField]int _selectNumber;
    private void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    _playerNumber = _selectNumber;
        //    Debug.Log(_playerNumber);
        //}
        if (Input.GetKeyDown(KeyCode.Mouse1))
        { Debug.Log(_playerNumber); }
    }
    public void OnPointerClick(PointerEventData eventData)
    {
        _playerNumber =_selectNumber;
        Debug.Log(_playerNumber);
    }
}
