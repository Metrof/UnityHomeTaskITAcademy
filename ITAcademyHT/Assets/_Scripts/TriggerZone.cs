using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class TriggerZone : MonoBehaviour
{
    [SerializeField] bool _isReusable;
    [SerializeField] TriggerObject _triggerObject;

    public void SetTriggerObject(TriggerObject trigger)
    {
        _triggerObject = trigger;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            _triggerObject.TriggerActivate();
            if (!_isReusable) gameObject.SetActive(false);
        }
    }
}
