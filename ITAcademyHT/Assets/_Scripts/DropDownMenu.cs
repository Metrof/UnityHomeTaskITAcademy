using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DropDownMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    public void SwitchText(int value)
    {
        switch (value)
        {
            case 0:
                _text.text = "Option A";
                break;
            case 1:
                _text.text = "Option B";
                break;
            case 2:
                _text.text = "Option C";
                break;
        }
    }
}
