using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burret : MonoBehaviour
{
    private void Start()
    {
        Destroy(gameObject, 7f);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {   
        gameObject.transform.position = new Vector3(1000, 1000, 1000);
    }
}
