using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// 発射されるものにつける
/// </summary>
public class Burret : MonoBehaviour
{
    /// <summary>
    /// 7秒後に死ぬ君
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    /// <summary>
    /// ものに当たったらとりあえずどっか飛ばしとく
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
    }
}
