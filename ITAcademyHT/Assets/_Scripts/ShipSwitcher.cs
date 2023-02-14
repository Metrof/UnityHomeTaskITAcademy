using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShipSwitcher : MonoBehaviour
{
    [SerializeField] float _movementCycleTime = 1;
    [SerializeField] Button _leftButton;
    [SerializeField] Button _rightButton;

    ShipsWheel _wheel;
    Transform _centerWheel;
    float _startSwitchTime;
    float _switchAngle;
    bool _isSwitching;

    int _currentShip;
    public int CurrentShip
    {
        get { return _currentShip; }
        set 
        {
            if (value >= _wheel.GetShipsCount())
            {
                _currentShip = 0;
            } else if (value <= -1)
            {
                _currentShip = _wheel.GetShipsCount() - 1;
            }
            else
            {
                _currentShip = value;
            }
        }
    }
    private void Start() 
    {
        ChangeAngleRotation();

        CurrentShip = 0;
        _centerWheel.rotation = Quaternion.Euler(0, _centerWheel.rotation.y + _switchAngle, 0);
    }
    public void Initialized(ShipsWheel wheel)
    {
        _wheel = wheel;
        _centerWheel = _wheel.GetCenterWheel();
        _leftButton.onClick.AddListener(SwipeLeft);
        _rightButton.onClick.AddListener(SwipeRight);
    }
    private void SwipeLeft() => StartCoroutine(ShipSwipeCorotine(-1));
    private void SwipeRight() => StartCoroutine(ShipSwipeCorotine(1));
    private void ChangeAngleRotation()
    {
        Vector3 dir = _centerWheel.transform.position - _wheel.GetShip(CurrentShip).transform.position;
        _switchAngle = Vector3.Angle(dir, _centerWheel.transform.position - new Vector3(transform.position.x, 0, transform.position.z));
    }
    IEnumerator ShipSwipeCorotine(int direction)
    {
        if (_isSwitching) yield break;

        _isSwitching = true;
        CurrentShip += direction;
        _startSwitchTime = Time.time;

        ChangeAngleRotation();
        float currentAngle = _centerWheel.rotation.eulerAngles.y;
        Quaternion targetRotation = Quaternion.Euler(0, currentAngle + _switchAngle * direction, 0);
        Quaternion startRotation = _centerWheel.rotation;
        float delta = 0;

        while (delta != 1)
        {
            delta = Mathf.Clamp01(Mathf.Abs((Time.time - _startSwitchTime) / _movementCycleTime));
            _centerWheel.rotation = Quaternion.Lerp(startRotation, targetRotation, delta);
            yield return null;
        }
        _isSwitching = false;
    }
    //private void ChangeAngleRotation()
    //{
    //    Vector3 dir = _centerWheel.transform.position - _wheel.GetShip(CurrentShip).transform.position;
    //    print(dir + " Before Normalize");
    //    dir.Normalize();
    //    print(dir);
    //    Vector3 rotMove = Quaternion.Euler(0, _centerWheel.transform.rotation.eulerAngles.y, 0) * dir;
    //    _switchAngle = Mathf.Atan2(rotMove.x, rotMove.z) * Mathf.Rad2Deg;
    //}
    //private Quaternion GetTargetRotation()
    //{
    //    print(_switchAngle + " _switchAngle");
    //    print(CurrentShip + " CurrentShip");
    //    return Quaternion.Euler(0, _switchAngle, 0);
    //}
}
