using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu : MonoBehaviour
{
    [SerializeField]static int _playerNumber=0;
    static int _playerName;
    [SerializeField]int _selectNumber;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            _playerNumber = _selectNumber;
            Debug.Log(_playerNumber);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        { Debug.Log(_playerNumber); }
    }
}
