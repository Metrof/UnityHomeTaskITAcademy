using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Cinemachine;

public class GameManager : MonoBehaviour
{
    [SerializeField] Transform _playerStartPos;
    [SerializeField] Player _playerPref;
    [SerializeField] CinemachineVirtualCamera _playerVirtualCamera;
    [SerializeField] TextMeshProUGUI _deathText;
    [SerializeField] TriggerZone _deathZone;

    private int _deathCount;
    private Player _player;

    private void Start()
    {
        _deathCount = 0;
        _player = Instantiate(_playerPref, _playerStartPos.position, Quaternion.identity);
        _player.OnDeath += PlayerDeath;
        _playerVirtualCamera.Follow = _player.transform;
        _deathZone.SetTriggerObject(_player);
    }

    private void PlayerDeath()
    {
        _player.PlayerTransform(_playerStartPos.position);
        UGUIUpdate();
    }
    private void UGUIUpdate()
    {
        _deathCount++;
        _deathText.text = "Death: " + _deathCount;
    }
}
