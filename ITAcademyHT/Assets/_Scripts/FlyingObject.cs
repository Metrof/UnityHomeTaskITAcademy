using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float _flyingSpeed = 4;
    [SerializeField] float _startRandomDir = 5;
    [SerializeField] Mesh[] _meshes;

    private Rigidbody _rb;
    private Renderer _renderer;
    private MeshFilter _meshFilter;
    private Vector3 _direction;

    private readonly IBehaviorStrategy[] _strategies = new IBehaviorStrategy[3] { new ScaleChanger(), new ColorChanger(), new FormChanger() };
    private IBehaviorStrategy _thisObjectStrategy;

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

    public Renderer ObjectRenderer { get { return _renderer; } }
    public MeshFilter ObjectMeshFilter { get { return _meshFilter; } }
    public Mesh[] Meshes { get { return _meshes; } }

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _renderer = GetComponent<Renderer>();
        _rb = GetComponent<Rigidbody>();
    }

    private void Start()
    {
        _thisObjectStrategy = _strategies[Random.Range(0, _strategies.Length)];
        Direcrion = new Vector3(Random.Range(-_startRandomDir, _startRandomDir), 0, Random.Range(-_startRandomDir, _startRandomDir)).normalized;
    }

    private void Update()
    {
        if (Physics.Raycast(transform.position, transform.forward, out RaycastHit hit, 0.8f, LayerMask.GetMask("Default")))
        {
            Direcrion = Vector3.Reflect(_direction, hit.normal).normalized;
            _thisObjectStrategy.ChangeObject(this);
        }
    }
}
