using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemKaiten : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            transform.Rotate(2, 0, 90, Space.Self);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            transform.Rotate(0, 0, -90, Space.Self);
        }

    }
}
