using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewChanger : MonoBehaviour
{
    [SerializeField] Transform _observer;
    [SerializeField] Transform _observablePoint;
    [SerializeField] Vector3 _observerPosition;

    Button _button;

    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(SwitchView);
    }

    private void SwitchView()
    {
        _observer.position = _observerPosition;
        _observer.LookAt(_observablePoint.position);
    }
}
