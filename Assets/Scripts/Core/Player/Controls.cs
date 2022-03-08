// GENERATED AUTOMATICALLY FROM 'Assets/Scripts/Core/Player/Controls.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

public class @Controls : IInputActionCollection, IDisposable
{
    public InputActionAsset asset { get; }
    public @Controls()
    {
        asset = InputActionAsset.FromJson(@"{
    ""name"": ""Controls"",
    ""maps"": [
        {
            ""name"": ""Gameplay"",
            ""id"": ""28787deb-4371-4dcd-bb01-6de9dffcb973"",
            ""actions"": [
                {
                    ""name"": ""Movement"",
                    ""type"": ""Button"",
                    ""id"": ""53fae35b-b896-4d3b-bd24-1d5579eb8580"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse"",
                    ""type"": ""Value"",
                    ""id"": ""a3685aac-245d-4009-8122-16d3b968391f"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""0852809a-7347-415e-a4ee-24790eba8b15"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Crouch"",
                    ""type"": ""Button"",
                    ""id"": ""26b47cb5-ccd3-4298-925b-b57fba81ca3d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interract"",
                    ""type"": ""Button"",
                    ""id"": ""24714152-63db-455a-b293-0980d22a2f91"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""MouseClick"",
                    ""type"": ""Button"",
                    ""id"": ""9b14133d-6583-4ea2-8729-9d087ba593ba"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Dash"",
                    ""type"": ""Button"",
                    ""id"": ""e6d7778c-601a-4fca-a885-a650a64a8bda"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload Dungeon"",
                    ""type"": ""Button"",
                    ""id"": ""f258c1ca-d5e3-4571-9819-ad2cbfa1b104"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Escape"",
                    ""type"": ""Button"",
                    ""id"": ""0fef1869-177a-4812-9a7f-e108abbadab3"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Scroll"",
                    ""type"": ""PassThrough"",
                    ""id"": ""4e1b4e5d-8b0d-40d4-92c7-8de3abe18b56"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Inventory"",
                    ""type"": ""Button"",
                    ""id"": ""9543ab50-e8ee-493d-85d1-cbc46fbe9c4f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Mouse Position"",
                    ""type"": ""Value"",
                    ""id"": ""26539ae2-487f-4ebb-842b-f62693b8b397"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""TAB"",
                    ""type"": ""Button"",
                    ""id"": ""09470348-8dae-4c9e-ac7d-bd28425d6245"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Attributes"",
                    ""type"": ""Button"",
                    ""id"": ""0ab9af1a-4903-4597-a362-c1e4b751e12c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": ""WASD"",
                    ""id"": ""fe046ef9-91fd-490b-826f-e402c0463d69"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""bd8585fd-437e-4a96-890a-8a19c1abd991"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""d77b7de3-a000-4a43-87e0-95a9491edb93"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""f84065cf-0d20-4763-9c38-25205d33985d"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""7888ab7c-b0de-42f5-ae50-be14baa1cb94"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Movement"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3b7c4cab-b501-4029-8136-17442e451931"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b53afb59-2cfb-4d03-a889-af4312712146"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a901988d-f6e6-46eb-9521-0af254f7b52b"",
                    ""path"": ""<Keyboard>/leftCtrl"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Crouch"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""3204c3f5-5685-4586-bb39-736b99b120d8"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Interract"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a74a73bc-345f-424a-b9f4-03097d816bf7"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""MouseClick"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a5f88870-2aef-49db-9d00-c0ea97ec7285"",
                    ""path"": ""<Keyboard>/leftShift"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Dash"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d08b117c-b56a-41a9-8c2f-233f6e262c90"",
                    ""path"": ""<Keyboard>/backspace"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload Dungeon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eee7100a-9e56-4508-bca4-adb86047a9af"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Escape"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""30ea9505-a33d-4afa-ac27-8da8a91319fe"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Scroll"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e82b2f81-284a-496d-aa7f-12bbfa925709"",
                    ""path"": ""<Keyboard>/i"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Inventory"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f67cfa4e-e29a-47fa-8d22-10d0ba301042"",
                    ""path"": ""<Mouse>/position"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Mouse Position"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""2a6f262c-b7e5-4d40-86ab-c40fd5be21a1"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""TAB"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""9f4fc9e5-71e1-4125-a2c2-d05e1b880711"",
                    ""path"": ""<Keyboard>/b"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Attributes"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": []
}");
        // Gameplay
        m_Gameplay = asset.FindActionMap("Gameplay", throwIfNotFound: true);
        m_Gameplay_Movement = m_Gameplay.FindAction("Movement", throwIfNotFound: true);
        m_Gameplay_Mouse = m_Gameplay.FindAction("Mouse", throwIfNotFound: true);
        m_Gameplay_Jump = m_Gameplay.FindAction("Jump", throwIfNotFound: true);
        m_Gameplay_Crouch = m_Gameplay.FindAction("Crouch", throwIfNotFound: true);
        m_Gameplay_Interract = m_Gameplay.FindAction("Interract", throwIfNotFound: true);
        m_Gameplay_MouseClick = m_Gameplay.FindAction("MouseClick", throwIfNotFound: true);
        m_Gameplay_Dash = m_Gameplay.FindAction("Dash", throwIfNotFound: true);
        m_Gameplay_ReloadDungeon = m_Gameplay.FindAction("Reload Dungeon", throwIfNotFound: true);
        m_Gameplay_Escape = m_Gameplay.FindAction("Escape", throwIfNotFound: true);
        m_Gameplay_Scroll = m_Gameplay.FindAction("Scroll", throwIfNotFound: true);
        m_Gameplay_Inventory = m_Gameplay.FindAction("Inventory", throwIfNotFound: true);
        m_Gameplay_MousePosition = m_Gameplay.FindAction("Mouse Position", throwIfNotFound: true);
        m_Gameplay_TAB = m_Gameplay.FindAction("TAB", throwIfNotFound: true);
        m_Gameplay_Attributes = m_Gameplay.FindAction("Attributes", throwIfNotFound: true);
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

    // Gameplay
    private readonly InputActionMap m_Gameplay;
    private IGameplayActions m_GameplayActionsCallbackInterface;
    private readonly InputAction m_Gameplay_Movement;
    private readonly InputAction m_Gameplay_Mouse;
    private readonly InputAction m_Gameplay_Jump;
    private readonly InputAction m_Gameplay_Crouch;
    private readonly InputAction m_Gameplay_Interract;
    private readonly InputAction m_Gameplay_MouseClick;
    private readonly InputAction m_Gameplay_Dash;
    private readonly InputAction m_Gameplay_ReloadDungeon;
    private readonly InputAction m_Gameplay_Escape;
    private readonly InputAction m_Gameplay_Scroll;
    private readonly InputAction m_Gameplay_Inventory;
    private readonly InputAction m_Gameplay_MousePosition;
    private readonly InputAction m_Gameplay_TAB;
    private readonly InputAction m_Gameplay_Attributes;
    public struct GameplayActions
    {
        private @Controls m_Wrapper;
        public GameplayActions(@Controls wrapper) { m_Wrapper = wrapper; }
        public InputAction @Movement => m_Wrapper.m_Gameplay_Movement;
        public InputAction @Mouse => m_Wrapper.m_Gameplay_Mouse;
        public InputAction @Jump => m_Wrapper.m_Gameplay_Jump;
        public InputAction @Crouch => m_Wrapper.m_Gameplay_Crouch;
        public InputAction @Interract => m_Wrapper.m_Gameplay_Interract;
        public InputAction @MouseClick => m_Wrapper.m_Gameplay_MouseClick;
        public InputAction @Dash => m_Wrapper.m_Gameplay_Dash;
        public InputAction @ReloadDungeon => m_Wrapper.m_Gameplay_ReloadDungeon;
        public InputAction @Escape => m_Wrapper.m_Gameplay_Escape;
        public InputAction @Scroll => m_Wrapper.m_Gameplay_Scroll;
        public InputAction @Inventory => m_Wrapper.m_Gameplay_Inventory;
        public InputAction @MousePosition => m_Wrapper.m_Gameplay_MousePosition;
        public InputAction @TAB => m_Wrapper.m_Gameplay_TAB;
        public InputAction @Attributes => m_Wrapper.m_Gameplay_Attributes;
        public InputActionMap Get() { return m_Wrapper.m_Gameplay; }
        public void Enable() { Get().Enable(); }
        public void Disable() { Get().Disable(); }
        public bool enabled => Get().enabled;
        public static implicit operator InputActionMap(GameplayActions set) { return set.Get(); }
        public void SetCallbacks(IGameplayActions instance)
        {
            if (m_Wrapper.m_GameplayActionsCallbackInterface != null)
            {
                @Movement.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Movement.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMovement;
                @Mouse.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Mouse.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouse;
                @Jump.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Jump.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnJump;
                @Crouch.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Crouch.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnCrouch;
                @Interract.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInterract;
                @Interract.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInterract;
                @Interract.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInterract;
                @MouseClick.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @MouseClick.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @MouseClick.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMouseClick;
                @Dash.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @Dash.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnDash;
                @ReloadDungeon.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadDungeon;
                @ReloadDungeon.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadDungeon;
                @ReloadDungeon.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnReloadDungeon;
                @Escape.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                @Escape.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                @Escape.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnEscape;
                @Scroll.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScroll;
                @Scroll.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScroll;
                @Scroll.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnScroll;
                @Inventory.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @Inventory.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnInventory;
                @MousePosition.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @MousePosition.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnMousePosition;
                @TAB.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTAB;
                @TAB.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTAB;
                @TAB.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnTAB;
                @Attributes.started -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttributes;
                @Attributes.performed -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttributes;
                @Attributes.canceled -= m_Wrapper.m_GameplayActionsCallbackInterface.OnAttributes;
            }
            m_Wrapper.m_GameplayActionsCallbackInterface = instance;
            if (instance != null)
            {
                @Movement.started += instance.OnMovement;
                @Movement.performed += instance.OnMovement;
                @Movement.canceled += instance.OnMovement;
                @Mouse.started += instance.OnMouse;
                @Mouse.performed += instance.OnMouse;
                @Mouse.canceled += instance.OnMouse;
                @Jump.started += instance.OnJump;
                @Jump.performed += instance.OnJump;
                @Jump.canceled += instance.OnJump;
                @Crouch.started += instance.OnCrouch;
                @Crouch.performed += instance.OnCrouch;
                @Crouch.canceled += instance.OnCrouch;
                @Interract.started += instance.OnInterract;
                @Interract.performed += instance.OnInterract;
                @Interract.canceled += instance.OnInterract;
                @MouseClick.started += instance.OnMouseClick;
                @MouseClick.performed += instance.OnMouseClick;
                @MouseClick.canceled += instance.OnMouseClick;
                @Dash.started += instance.OnDash;
                @Dash.performed += instance.OnDash;
                @Dash.canceled += instance.OnDash;
                @ReloadDungeon.started += instance.OnReloadDungeon;
                @ReloadDungeon.performed += instance.OnReloadDungeon;
                @ReloadDungeon.canceled += instance.OnReloadDungeon;
                @Escape.started += instance.OnEscape;
                @Escape.performed += instance.OnEscape;
                @Escape.canceled += instance.OnEscape;
                @Scroll.started += instance.OnScroll;
                @Scroll.performed += instance.OnScroll;
                @Scroll.canceled += instance.OnScroll;
                @Inventory.started += instance.OnInventory;
                @Inventory.performed += instance.OnInventory;
                @Inventory.canceled += instance.OnInventory;
                @MousePosition.started += instance.OnMousePosition;
                @MousePosition.performed += instance.OnMousePosition;
                @MousePosition.canceled += instance.OnMousePosition;
                @TAB.started += instance.OnTAB;
                @TAB.performed += instance.OnTAB;
                @TAB.canceled += instance.OnTAB;
                @Attributes.started += instance.OnAttributes;
                @Attributes.performed += instance.OnAttributes;
                @Attributes.canceled += instance.OnAttributes;
            }
        }
    }
    public GameplayActions @Gameplay => new GameplayActions(this);
    public interface IGameplayActions
    {
        void OnMovement(InputAction.CallbackContext context);
        void OnMouse(InputAction.CallbackContext context);
        void OnJump(InputAction.CallbackContext context);
        void OnCrouch(InputAction.CallbackContext context);
        void OnInterract(InputAction.CallbackContext context);
        void OnMouseClick(InputAction.CallbackContext context);
        void OnDash(InputAction.CallbackContext context);
        void OnReloadDungeon(InputAction.CallbackContext context);
        void OnEscape(InputAction.CallbackContext context);
        void OnScroll(InputAction.CallbackContext context);
        void OnInventory(InputAction.CallbackContext context);
        void OnMousePosition(InputAction.CallbackContext context);
        void OnTAB(InputAction.CallbackContext context);
        void OnAttributes(InputAction.CallbackContext context);
    }
}
