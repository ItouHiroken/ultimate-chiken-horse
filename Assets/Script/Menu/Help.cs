using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class Help : MonoBehaviour
{
    TextMeshProUGUI _text;
    [SerializeField] int _page;
    int i;
    private void Start()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }
    private void Update()
    {
        switch (i)
        {
            case 0:
                _text.text = "ActionTurn\n\n\nMove�cLeftStick\nJump�cB";
                break;
            case 1:
                _text.text = "ChoiceItem\n\n\nMove�cLeftStick\nChoice�cA";
                break;
            case 2:
                _text.text = "SetItem\n\n\nMove�cLeftStick\nSet�cA\nRotate�cLB RB";
                break;
        }
    }

    public void PageUp()
    {
        if (i != _page)
        {
            i += 1;
        }
    }
    public void PageDown()
    {
        if (i != 0)
        {
            i -= 1;
        }
    }
}
