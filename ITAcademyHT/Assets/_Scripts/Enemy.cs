using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(Collider2D))]
public class Enemy : MonoBehaviour
{
    [SerializeField] float _knockbackForce = 10;
    [SerializeField] float _enemyMovementTime = 3;
    [SerializeField] float _movementDistance = 1;
    Vector2 _APos;
    Vector2 _BPos;

    float _startMovingFromPointTime;

    private void Start()
    {
        _APos = _BPos = transform.localPosition;
        _APos.x = transform.localPosition.x + _movementDistance;
        _BPos.x = transform.localPosition.x - _movementDistance;

        _startMovingFromPointTime = Time.time;
    }

    private void FixedUpdate()
    {
        float delta = Mathf.Abs((Time.time - _startMovingFromPointTime) / _enemyMovementTime);

        transform.localPosition = Vector2.Lerp(_APos, _BPos, delta);

        if (delta >= 1) _startMovingFromPointTime = Time.time + _enemyMovementTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Player player))
        {
            if ((collision.transform.position.y - transform.position.y) > 0.5f)
            {
                player.Rigidbody.AddForce(Vector2.up * _knockbackForce, ForceMode2D.Impulse);
                Destroy(gameObject);
            }
            else
            {
                player.TriggerActivate();
            }
        }
    }
}
