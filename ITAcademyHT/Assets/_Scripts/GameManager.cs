using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject _mainBlock;
    [SerializeField] Material _capMaterial;

    private float _currentXLarge;
    private float _currentZLarge;
    private float _currentYLarge;
    private float _disToMainBlock;
    private Vector3 _startPoint = Vector3.right * 2;
    private GameObject _currentCube;
    private GameObject _previosCube;

    private CubeGenerator _cubeGenerator;

    Controller _inputActions;

    private void Awake()
    {
        _inputActions = new Controller();
    }
    private void Start()
    {
        _cubeGenerator = GetComponent<CubeGenerator>();
        UpdateParram(_mainBlock);
    }
    private void OnEnable()
    {
        _inputActions.Enable();
        _inputActions.Game.PutBlock.performed += Slice;
    }
    private void OnDisable()
    {
        _inputActions.Game.PutBlock.performed -= Slice;
        _inputActions.Disable();
    }

    private void Slice(InputAction.CallbackContext obj)
    {
        GameObject[] sliced = BlockCut(_currentCube);

        foreach (var i in sliced)
        {
            i.name = string.Format("{0} ({1})", gameObject.name, i.name);

            if (i.GetComponent<Rigidbody>() == null)
            {
                i.AddComponent<Rigidbody>();
            }
        }
    }

    private void UpdateParram(GameObject previosBox)
    {
        _currentXLarge = previosBox.transform.localScale.x;
        _currentZLarge = previosBox.transform.localScale.z;
        _currentYLarge = previosBox.transform.localScale.y;
        _startPoint.y += _currentYLarge;
        (_startPoint.x, _startPoint.z) = (_startPoint.z, _startPoint.x);
        SpawnBlock();
    }
    private void SpawnBlock()
    {
        //if (_previosMesh != null)
        //{
        //    _currentCube = _cubeGenerator.CreateCube(_previosMesh, _capMaterial);
        //}
        //else
        //{
        //    _currentCube = _cubeGenerator.CreateCube(new Vector3(_currentXLarge, _currentYLarge, _currentZLarge), _capMaterial);
        //}
        _previosCube = _currentCube;
        _currentCube = _cubeGenerator.CreateCube(new Vector3(_currentXLarge, _currentYLarge, _currentZLarge), _capMaterial);
        _currentCube.AddComponent<BoxCollider>();
        _currentCube.AddComponent<CubeMovement>().SetMoveDirection(new Vector3(-_startPoint.x, 0, -_startPoint.z));
        _currentCube.transform.position = _startPoint;
    }
    public GameObject[] BlockCut(GameObject victim)
    {
        if (_startPoint.x < 0)
        {
            _disToMainBlock = _previosCube.transform.localScale.x - _currentCube.transform.position.x;

        }
        else
        {

        }
        return new GameObject[0];
    }
}
