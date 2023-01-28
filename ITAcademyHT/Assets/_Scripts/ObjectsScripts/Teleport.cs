using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    [SerializeField] float _timeBetweenTeleport = 3;
    [SerializeField] float _teleportationDurationTime = 1;

    readonly float _minXPos = -5;
    readonly float _maxXPos = 5;
    readonly float _minZPos = -6;
    readonly float _maxZPos = 0;

    float _nextTeleportationTime;

    private void Start()
    {
        _nextTeleportationTime = Time.time + _timeBetweenTeleport;
        StartCoroutine(TeleportationCorotine());
    }

    private void Update()
    {
        if (Time.time >= _nextTeleportationTime) StartCoroutine(TeleportationCorotine());
    }

    IEnumerator TeleportationCorotine()
    {
        Vector3 newPosition = new(Random.Range(_minXPos, _maxXPos), 1, Random.Range(_minZPos, _maxZPos));
        float halfDuration = _teleportationDurationTime / 2;
        transform.position = newPosition;
        _nextTeleportationTime = Time.time + _timeBetweenTeleport;
        yield return new WaitForSeconds(_timeBetweenTeleport - halfDuration);

        float endTeleportationTime = Time.time + _teleportationDurationTime;
        while (endTeleportationTime > Time.time)
        {
            float delta = Mathf.Abs((Time.time - (endTeleportationTime - halfDuration)) / (halfDuration));
            transform.localScale = Vector3.one * delta;
            yield return null;
        }
    }
}
