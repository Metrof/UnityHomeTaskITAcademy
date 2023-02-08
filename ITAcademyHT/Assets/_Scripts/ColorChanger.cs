using UnityEngine;
using UnityEngine.UI;

public class ColorChanger : MonoBehaviour
{
    [SerializeField] MenuManager _manager;
    Button _colorButton;
    Color _buttonColor;

    private void Awake()
    {
        _colorButton = GetComponent<Button>();
        _buttonColor = _colorButton.GetComponent<Image>().color;
        _colorButton.onClick.AddListener(SwitchColor);
    }
    private void SwitchColor()
    {
        _manager.SwitchCurrentShipColor(_buttonColor);
    }
}
