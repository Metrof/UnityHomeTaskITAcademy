using UnityEngine;

public class Background : MonoBehaviour
{
    [SerializeField] private float _groundSpeed = 0.2f;

    private Player _player;
    private Material m_Material;
    private float _distance;

    private void Awake()
    {
        _player = FindObjectOfType<Player>();
        m_Material = GetComponent<Renderer>().material;
    }

    private void OnEnable()
    {
        _player.OnMove += Move;
    }

    private void OnDisable()
    {
        _player.OnMove -= Move;
    }

    private void Move(float axis)
    {
        _distance += _groundSpeed * _player.Speed * Time.deltaTime * axis;
        if (m_Material != null) m_Material.SetTextureOffset("_MainTex", Vector2.right * _distance);
    }
}
