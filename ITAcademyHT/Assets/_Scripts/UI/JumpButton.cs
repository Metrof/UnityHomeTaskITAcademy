using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class JumpButton : MonoBehaviour
{
    [SerializeField] PlayerControll _player;
    Button _jumpButton;

    private void Awake()
    {
        _jumpButton = GetComponent<Button>();
        _jumpButton.onClick.AddListener(PressJump);
    }
    private void PressJump()
    {
        _player.Jump();
    }
}
