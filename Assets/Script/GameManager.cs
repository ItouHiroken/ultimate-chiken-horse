using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public Turn NowTurn;
    void TurnChange()
    {
    }
    public enum Turn
    { 
        GamePlay,
        Result,
        SelectItem,
        SetItem,
    }
}
