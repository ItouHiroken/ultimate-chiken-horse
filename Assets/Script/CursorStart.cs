using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームマネージャーからの指示でカーソルを定位置につかせる
/// </summary>
public class CursorStart : MonoBehaviour
{
    [SerializeField, Tooltip("カーソルたち")] List<GameObject> _cursors = new();
    [SerializeField, Tooltip("カーソルの定位置")] List<GameObject> _position = new();
    public bool SelectSceneStart;
    private void Update()
    {
        if (SelectSceneStart == true)
        {
            for (int i = 0; i < Menu._playerNumber; i++)
            {
                _cursors[i].transform.position = _position[i].transform.position;//カーソルを定位置に移動させる
                _cursors[i].GetComponent<PlayerCursor>().enabled = true;//カーソルが動くようにする
            }
            SelectSceneStart = false;
        }
    }
}
