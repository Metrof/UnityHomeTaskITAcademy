using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BG : MonoBehaviour
{
    Material m_Material;
    float _distance;

    [SerializeField] float _speed = 0.5f;

    private void Awake()
    {
        m_Material = GetComponent<Renderer>().material;
    }

    private void Update()
    {
        _distance = Time.deltaTime * _speed;
        m_Material.SetTextureOffset("_MainTex", Vector2.right * _distance);
    }
}
