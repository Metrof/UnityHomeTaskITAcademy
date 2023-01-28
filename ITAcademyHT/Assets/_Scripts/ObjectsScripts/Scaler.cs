using UnityEngine;

public class Scaler : MonoBehaviour
{
    [SerializeField] GameObject[] _beads;
    [SerializeField] GameObject _gravityObject;
    [SerializeField] float _radius = 3;
    [SerializeField] float _minDistanceForDecrease = 3;
    [SerializeField][Range(0.01f, 2)] float _speed = 0.3f;

    float _delta;
    Vector3 _centerBeads;

    void Start()
    {
        _centerBeads = transform.position;
        for (int i = 0; i < _beads.Length; i++)
        {
            float circlePosition = i / (float)_beads.Length;
            Vector3 pos = _beads[i].transform.position;
            pos.x += Mathf.Sin(circlePosition * Mathf.PI * 2.0f) * _radius;
            pos.z += Mathf.Cos(circlePosition * Mathf.PI * 2.0f) * _radius;
            _beads[i].transform.position = pos;
        }
    }

    void Update()
    {
        if (_beads.Length <= 0) return;

        _delta += _speed * Time.deltaTime % 1;
        float xPos = Mathf.Sin(_delta * Mathf.PI * 2.0f) * _radius;
        float zPos = Mathf.Cos(_delta * Mathf.PI * 2.0f) * _radius;
        _gravityObject.transform.position = new Vector3(_centerBeads.x + xPos, _centerBeads.y + 0.5f, _centerBeads.z + zPos);

        for (int i = 0; i < _beads.Length; i++)
        {
            float distance = Vector3.Distance(_gravityObject.transform.position, _beads[i].transform.position);
            if (distance <= _minDistanceForDecrease)
            {
                float gravityScale = Mathf.Clamp(distance / _minDistanceForDecrease, 0.1f, 1);
                _beads[i].transform.localScale = new Vector3(gravityScale, gravityScale, gravityScale);
            }
            else
            {
                _beads[i].transform.localScale = Vector3.one;
            }
        }
    }
}
