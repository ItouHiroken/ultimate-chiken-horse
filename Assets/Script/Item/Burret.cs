using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ���˂������̂ɂ���
/// </summary>
public class Burret : MonoBehaviour
{
    /// <summary>
    /// 7�b��Ɏ��ʌN
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    /// <summary>
    /// ���̂ɓ���������Ƃ肠�����ǂ�����΂��Ƃ�
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
    }
}
