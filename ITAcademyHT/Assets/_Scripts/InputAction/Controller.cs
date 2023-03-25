//------------------------------------------------------------------------------
// <auto-generated>
//     This code was auto-generated by com.unity.inputsystem:InputActionCodeGenerator
//     version 1.4.4
//     from Assets/_Scripts/InputAction/Controller.inputactions
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public partial class @Controller : IInputActionCollection2, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controller()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controller"",
    ""maps"": [
        {
            ""name"": ""Player"",
            ""id"": ""3c1d5837-886c-41b8-94ae-665dfaec4df9"",
            ""actions"": [
                {
                    ""name"": ""SetDestination"",
                    ""type"": ""Button"",
                    ""id"": ""b5537584-d55b-4eee-ac6b-33227fc2726e"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """",
                    ""initialStateCheck"": false
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""29776dae-424c-433e-a6f1-88c18312a5ab"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""KeyBoard_and_Mouse"",
                    ""action"": ""SetDestination"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""KeyBoard_and_Mouse"",
            ""bindingGroup"": ""KeyBoard_and_Mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
        // Player
        m_Player = asset.FindActionMap("Player", throwIfNotFound: true);
        m_Player_SetDestination = m_Player.FindAction("SetDestination", throwIfNotFound: true);
    }

    public void Dispose()
    {
        UnityEngine.Object.Destroy(asset);
    }

    public InputBinding? bindingMask
    {
        get => asset.bindingMask;
        set => asset.bindingMask = value;
    }

    public ReadOnlyArray<InputDevice>? devices
    {
        get => asset.devices;
        set => asset.devices = value;
    }

    public ReadOnlyArray<InputControlScheme> controlSchemes => asset.controlSchemes;

    public bool Contains(InputAction action)
    {
        return asset.Contains(action);
    }

    public IEnumerator<InputAction> GetEnumerator()
    {
        return asset.GetEnumerator();
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    public void Enable()
    {
        asset.Enable();
    }

    public void Disable()
    {
        asset.Disable();
    }
    public IEnumerable<InputBinding> bindings => asset.bindings;

    public InputAction FindAction(string actionNameOrId, bool throwIfNotFound = false)
    {
        return asset.FindAction(actionNameOrId, throwIfNotFound);
    }
    public int FindBinding(InputBinding bindingMask, out InputAction action)
    {
        return asset.FindBinding(bindingMask, out action);
    }

    // Player
    private readonly InputActionMap m_Player;
    private IPlayerActions m_PlayerActionsCallbackInterface;
    private readonly InputAction m_Player_SetDestination;
    public struct PlayerActions
    {
        private @Controller m_Wrapper;
        public PlayerActions(@Controller wrapper) { m_Wrapper = wrapper; }
        public InputAction @SetDestination => m_Wrapper.m_Player_SetDestination;
        public InputActionMap Get() { return m_Wrapper.m_Player; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(PlayerActions set) { return set.Get(); }
        public void SetCallbacks(IPlayerActions instance)
        {
            if (m_Wrapper.m_PlayerActionsCallbackInterface != null)
            {
                @SetDestination.started -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetDestination;
                @SetDestination.performed -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetDestination;
                @SetDestination.canceled -= m_Wrapper.m_PlayerActionsCallbackInterface.OnSetDestination;
            }
            m_Wrapper.m_PlayerActionsCallbackInterface = instance;
            if (instance != null)
            {
                @SetDestination.started += instance.OnSetDestination;
                @SetDestination.performed += instance.OnSetDestination;
                @SetDestination.canceled += instance.OnSetDestination;
            }
        }
    }
    public PlayerActions @Player => new PlayerActions(this);
    private int m_KeyBoard_and_MouseSchemeIndex = -1;
    public InputControlScheme KeyBoard_and_MouseScheme
    {
        get
        {
            if (m_KeyBoard_and_MouseSchemeIndex == -1) m_KeyBoard_and_MouseSchemeIndex = asset.FindControlSchemeIndex("KeyBoard_and_Mouse");
            return asset.controlSchemes[m_KeyBoard_and_MouseSchemeIndex];
        }
    }
    public interface IPlayerActions
    {
        void OnSetDestination(InputAction.CallbackContext context);
    }
}
