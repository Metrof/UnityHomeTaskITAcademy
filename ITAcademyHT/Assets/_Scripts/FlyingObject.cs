using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.HID;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float _flyingSpeed = 6;
    [SerializeField] float _startRandomDir = 5;
    [SerializeField] Mesh[] _meshes;

    private Rigidbody _rb;
    private Vector3 _direction;

    private readonly IBehaviorStrategy[] _strategies = new IBehaviorStrategy[3] { new ScaleChanger(), new ColorChanger(), new FormChanger() };
    private IBehaviorStrategy _thisObjectStrategy;

    public Mesh[] Meshes { get { return _meshes; } }

    private Vector3 Direcrion
    {
        get { return _direction * _flyingSpeed; }
        set
        {
            _direction = value;
            _rb.velocity = Direcrion;
            transform.LookAt(transform.position + _direction);
        }
    }

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _thisObjectStrategy = _strategies[Random.Range(0, _strategies.Length)];
        _thisObjectStrategy.AddObjectComponent(this);

        Direcrion = new Vector3(Random.Range(-_startRandomDir, _startRandomDir), 0, Random.Range(-_startRandomDir, _startRandomDir)).normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Direcrion = Vector3.Reflect(_direction, collision.contacts[0].normal).normalized;
            _thisObjectStrategy.ChangeObject();
        }
    }
}
