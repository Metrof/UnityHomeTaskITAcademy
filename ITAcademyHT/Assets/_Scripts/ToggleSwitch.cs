using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ToggleSwitch : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _text;
    [SerializeField] Toggle _toggle;
    void Start()
    {
        _toggle.onValueChanged.AddListener(ChangeToggleTitle);
    }

    private void ChangeToggleTitle(bool call)
    {
        if (call) _text.text = _toggle.name;
    }
}
