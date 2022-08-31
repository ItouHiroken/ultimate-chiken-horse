using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TitleButton : MonoBehaviour
{
    public void CloseCanvas()
    {
        Canvas canvas = transform.parent.gameObject.GetComponent<Canvas>();
        canvas.enabled = false;
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
