using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private Storey _storeyPrefab;
    [SerializeField] private Player _playerPrefab;
    [SerializeField] private int _storeyCount = 5;
    [SerializeField] private float _oneStoreyHeight = 7.8f;
    [SerializeField] private float _unstableLightPower = 7;
    [SerializeField] private float _unstableLightChangeSpeed = 6;
    [SerializeField] private Sprite[] _numberSprites;
    [SerializeField] private AudioClip[] _horrorClips;
    [SerializeField] private AudioSource _horrorAudioSource;
    [SerializeField] private Transform _startPlayerPos;
    [SerializeField] private CinemachineVirtualCamera _playerVirtualCamera;
    [SerializeField] private Light _unstableLight;

    private List<Storey> _storeys;
    private Player _player;

    private void Start()
    {
        _storeys = new List<Storey>();
        int multiple;
        int previosFlorNum = 0;
        int currentFlorNum;
        for (int i = 0; i < _storeyCount; i++)
        {
            multiple = i % 2 == 0 ? -1 : 1;
            currentFlorNum = i * multiple + previosFlorNum;
            Storey storey = Instantiate(_storeyPrefab, new Vector3(0, _oneStoreyHeight * currentFlorNum, 0), Quaternion.identity);
            storey.ChangeStoreyNum(currentFlorNum, _numberSprites[Mathf.Abs(currentFlorNum) / 10], _numberSprites[Mathf.Abs(currentFlorNum) % 10]);
            _storeys.Add(storey);
            previosFlorNum = currentFlorNum;
        }

        _player = Instantiate(_playerPrefab, _startPlayerPos.position, Quaternion.identity);
        _playerVirtualCamera.Follow = _player.CameraPos;
    }

    private void Update() 
    {
        if (_player.transform.position.y >= _startPlayerPos.position.y + _oneStoreyHeight)
        {
            WorldTranslate(1);
        }
        else if (_player.transform.position.y <= _startPlayerPos.position.y - _oneStoreyHeight)
        {
            WorldTranslate(-1);
        }

        _unstableLight.intensity = Mathf.Sin(_unstableLightChangeSpeed * Time.time) * _unstableLightPower;
    }

    private void WorldTranslate(int direction)
    {
        for (int i = 0; i < _storeys.Count; i++)
        {
            Sprite firstSprite = _numberSprites[Mathf.Clamp(Mathf.Abs(_storeys[i].StoreyNum + direction) / 10, 0, 9)];
            Sprite secondSprite = _numberSprites[Mathf.Clamp(Mathf.Abs(_storeys[i].StoreyNum + direction) % 10, 0, 9)];
            _storeys[i].ChangeStoreyNum(_storeys[i].StoreyNum + direction, firstSprite, secondSprite);
        }
        _player.transform.Translate(0f, _oneStoreyHeight * -direction, 0f, Space.World);

        _horrorAudioSource.PlayOneShot(_horrorClips[Random.Range(0, _horrorClips.Length)]);
    }
}
