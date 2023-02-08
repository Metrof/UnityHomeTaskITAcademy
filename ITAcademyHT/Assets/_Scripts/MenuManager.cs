using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ShipSwitcher))]
[RequireComponent(typeof(ShipsWheel))]
public class MenuManager : MonoBehaviour
{
    ShipSwitcher _switcher;
    ShipsWheel _shipsWheel;
    GameObject _interactiveShip;
    Renderer _interactiveRend;

    private void Awake()
    {
        _switcher = GetComponent<ShipSwitcher>();
        _shipsWheel = GetComponent<ShipsWheel>();

        _switcher.Initialized(_shipsWheel);
    }
    public void SwitchCurrentShipColor(Color color)
    {
        _interactiveRend = _shipsWheel.GetCurrentShipReng(_switcher.CurrentShip);
        _interactiveRend.material.color = color;
    }
    public void RotateCurrentShip(Vector3 dir, float angle)
    {
        _interactiveShip = _shipsWheel.GetShip(_switcher.CurrentShip);
        _interactiveShip.transform.Rotate(dir, angle, Space.World);
    }
}
