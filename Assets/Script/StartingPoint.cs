using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// ゲームマネージャーからの指示でプレイヤーの召喚と起動
/// </summary>
public class StartingPoint : MonoBehaviour
{
    [SerializeField] List<GameObject> _players = new();
    [SerializeField] List<GameObject> _position = new();
    public bool PlaySceneStart;
    private void Update()
    {
        if (PlaySceneStart == true)
        {
            for (int i = 0; i < Menu._playerNumber; i++)
            {
                _players[i].GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);//速度を一回0にする
                _players[i].transform.position = _position[i].transform.position;//プレイヤーを定位置に置く
                _players[i].GetComponent<PlayerMove>().enabled = true;//プレイヤーが動けるようにする
            }
            PlaySceneStart = false;
        }
    }
}