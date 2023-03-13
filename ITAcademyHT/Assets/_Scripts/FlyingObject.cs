using UnityEngine;

public class FlyingObject : MonoBehaviour
{
    [SerializeField] float _flyingSpeed = 6;
    [SerializeField] float _startRandomDir = 5;

    private Rigidbody _rb;
    private Vector3 _direction;
    private IObjectChanger _thisObjectStrategy;

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
        Direcrion = new Vector3(Random.Range(-_startRandomDir, _startRandomDir), 0, Random.Range(-_startRandomDir, _startRandomDir)).normalized;
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.CompareTag("Wall"))
        {
            Direcrion = Vector3.Reflect(_direction, collision.contacts[0].normal).normalized;
            _thisObjectStrategy?.ChangeObject();
        }
    }

    public void SetStrategy(IObjectChanger strategy)
    {
        _thisObjectStrategy = strategy;
    }
}
