using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipsWheel : MonoBehaviour
{
    [SerializeField] GameObject[] _ships;
    [SerializeField] Transform _shipParent;
    [SerializeField] float _radius = 50;

    Dictionary<GameObject, Renderer> _shipRend = new();

    private void Awake()
    {
        for (int i = 0; i < _ships.Length; i++)
        {
            _shipRend.Add(_ships[i], _ships[i].GetComponent<Renderer>());
        }
    }
    void Start()
    {
        PlacementShips();
    }
    private void PlacementShips()
    {
        if (_ships.Length > 0)
        {
            for (int i = 0; i < _ships.Length; i++)
            {
                float circlePosition = i / (float)_ships.Length;
                Vector3 pos = Vector3.zero;
                pos.x = Mathf.Cos(circlePosition * Mathf.PI * 2.0f) * _radius;
                pos.z = Mathf.Sin(circlePosition * Mathf.PI * 2.0f) * _radius;
                _ships[i].transform.localPosition = pos;
                _ships[i].transform.LookAt(_shipParent.position);
            }
        }
    }

    public GameObject GetShip(int indx)
    {
        return _ships[indx];
    }
    public Transform GetCenterWheel()
    {
        return _shipParent;
    }
    public int GetShipsCount()
    {
        return _ships.Length;
    }
    public Renderer GetCurrentShipReng(int currentShip)
    {
        return _shipRend[_ships[currentShip]];
    }
}
