using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Title : MonoBehaviour
{
    [SerializeField] Canvas EscapeCanvas;
    [SerializeField] Canvas TitleCanvas;
    [SerializeField] Canvas SelectCanvas;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscapeCanvas.enabled=!EscapeCanvas.enabled;
            EscapeCanvas.gameObject.SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            TitleCanvas.gameObject.SetActive(false);
            SelectCanvas.gameObject.SetActive(true);
        }
    }

}
