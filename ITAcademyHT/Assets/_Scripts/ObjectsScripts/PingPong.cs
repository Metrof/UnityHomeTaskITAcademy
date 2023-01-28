using UnityEngine;

public class PingPong : MonoBehaviour
{
    [SerializeField] float _movementCycleTime = 1;
    [SerializeField] float _movementDistance = 4;
    Vector3 _APos;
    Vector3 _BPos;

    float _startMovingFromPointTime;

    private void Start()
    {
        _APos = _BPos = transform.position;
        _APos.x += _movementDistance;
        _BPos.x -= _movementDistance;

        _startMovingFromPointTime = Time.time;
    }

    private void FixedUpdate()
    {
        float delta = Mathf.Abs((Time.time - _startMovingFromPointTime) / _movementCycleTime);

        transform.position = Vector3.Lerp(_APos, _BPos, delta);

        if (delta >= 1) _startMovingFromPointTime = Time.time + _movementCycleTime;
    }
}
