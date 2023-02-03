using UnityEngine;

public class Switcher : MonoBehaviour
{
    [SerializeField] GameObject[] _objects;
    GameObject _currentObj;
    Controls _controler;

    void Awake()
    {
        _controler = new Controls();
        _controler.Switcher.Switch.performed += context => Switch();
    }
    void OnEnable() => _controler.Enable();
    void OnDisable() => _controler.Disable();

    void Switch()
    {
        print("jj");
        if (_objects == null) return;
        if (_currentObj != null) Destroy(_currentObj);

        _currentObj = Instantiate(_objects[Random.Range(0, _objects.Length)]);
        _currentObj.transform.position = new Vector3(0, 1, -5);
    }
}
