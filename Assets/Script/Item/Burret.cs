using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// ”­ŽË‚³‚ê‚é‚à‚Ì‚É‚Â‚¯‚é
/// </summary>
public class Burret : MonoBehaviour
{
    /// <summary>
    /// 7•bŒã‚ÉŽ€‚ÊŒN
    /// </summary>
    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    /// <summary>
    /// ‚à‚Ì‚É“–‚½‚Á‚½‚ç‚Æ‚è‚ ‚¦‚¸‚Ç‚Á‚©”ò‚Î‚µ‚Æ‚­
    /// </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
    }
}
