// GENERATED AUTOMATICALLY FROM 'Assets/Akasha/InputActions/GameInputActions.inputactions'

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Utilities;

namespace Akasha.InputActions
{
    public class @GameInputActions : IInputActionCollection, IDisposable
    {
        public InputActionAsset asset { get; }
        public @GameInputActions()
        {
            asset = InputActionAsset.FromJson(@"{
    ""name"": ""GameInputActions"",
    ""maps"": [
        {
            ""name"": ""GameActionMap"",
            ""id"": ""c3282cce-54e9-49a1-a2b9-484c088bfb74"",
            ""actions"": [
                {
                    ""name"": ""Aim"",
                    ""type"": ""Button"",
                    ""id"": ""e3ca3e2b-c430-4181-ab5c-ad3f1bfe2f20"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Datapad"",
                    ""type"": ""Button"",
                    ""id"": ""62156231-5956-43be-82d5-5b358f55bcfd"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Hit"",
                    ""type"": ""Button"",
                    ""id"": ""342278cb-ea47-42b2-b766-3e5e20815531"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Interact"",
                    ""type"": ""Button"",
                    ""id"": ""ceffb010-a107-4de2-a99f-2f783ca27f1d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Jump"",
                    ""type"": ""Button"",
                    ""id"": ""9f79e8b7-041f-48e7-99cd-f3bc83d20dd6"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Look"",
                    ""type"": ""PassThrough"",
                    ""id"": ""313b8ae9-a4e6-434a-a534-bf5704a762bc"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Move"",
                    ""type"": ""PassThrough"",
                    ""id"": ""8c68db74-a06d-442f-8345-c222cb3011fd"",
                    ""expectedControlType"": ""Vector2"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Pause"",
                    ""type"": ""Button"",
                    ""id"": ""6af2dab6-16af-44d8-b5ec-55fa02eb2542"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Reload"",
                    ""type"": ""Button"",
                    ""id"": ""d83ca07b-2996-49ed-bc4f-6d848190e1ca"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectLastWeapon"",
                    ""type"": ""Button"",
                    ""id"": ""37d4166a-138d-4a27-b7ad-25e0b91652ac"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SelectWeapon"",
                    ""type"": ""PassThrough"",
                    ""id"": ""d85aebc6-ac4f-4ea0-b1f7-8e77d54b8826"",
                    ""expectedControlType"": ""Axis"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sneak"",
                    ""type"": ""Button"",
                    ""id"": ""b3c0a30b-6f39-47cd-bcb3-9894c75dec18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Sprint"",
                    ""type"": ""Button"",
                    ""id"": ""9d5b256e-1b47-4aff-8bbf-ab36b4f9e786"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SwitchView"",
                    ""type"": ""Button"",
                    ""id"": ""b708d9a0-eddd-4462-b8b5-2184ff010cf7"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""068ee17a-12bb-4f96-a879-8366f0980265"",
                    ""path"": ""<Gamepad>/leftStick"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""WASDKeys"",
                    ""id"": ""a29018fe-060b-46ae-8216-2b9785b60813"",
                    ""path"": ""2DVector"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Move"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""up"",
                    ""id"": ""05062532-92e6-460d-af4f-65d49d9cfbaf"",
                    ""path"": ""<Keyboard>/w"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""down"",
                    ""id"": ""96de8237-5c29-4118-ade1-3ff31d9bd842"",
                    ""path"": ""<Keyboard>/s"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""left"",
                    ""id"": ""7a43f27a-4d94-4ffc-9362-c468353d7796"",
                    ""path"": ""<Keyboard>/a"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""right"",
                    ""id"": ""ba722e60-c2a4-42bb-9400-6054ffee7436"",
                    ""path"": ""<Keyboard>/d"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Move"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""7cf2bdc3-bee3-44c9-a928-4d1e11a3697b"",
                    ""path"": ""<Gamepad>/leftTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""31a7a07b-f2fd-402d-bf18-aadb17ea0c5c"",
                    ""path"": ""<Keyboard>/shift"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Sprint"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""528938b3-5c4a-4307-bb00-9a6aaf9ab21a"",
                    ""path"": ""<Gamepad>/buttonSouth"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""136a4604-32d5-4187-b1da-79ea182d7484"",
                    ""path"": ""<Keyboard>/space"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Jump"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09ca7287-2ea7-4e99-afa2-e0722d29430b"",
                    ""path"": ""<Gamepad>/rightStick"",
                    ""interactions"": """",
                    ""processors"": ""InvertVector2(invertX=false)"",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""75218734-8b2e-4e12-9d4a-eb89e0617005"",
                    ""path"": ""<Mouse>/delta"",
                    ""interactions"": """",
                    ""processors"": ""ScaleVector2(y=-1)"",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Look"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""LeftAndRightShoulderButtons"",
                    ""id"": ""8e6ee9d2-c2ff-4359-b19c-84edc6b004be"",
                    ""path"": ""1DAxis"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SelectWeapon"",
                    ""isComposite"": true,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": ""negative"",
                    ""id"": ""b12edc9c-f728-4558-8c38-cde4089c27b4"",
                    ""path"": ""<Gamepad>/leftShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": ""positive"",
                    ""id"": ""8b00de62-194f-4340-89ee-224d7d9a54d6"",
                    ""path"": ""<Gamepad>/rightShoulder"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": true
                },
                {
                    ""name"": """",
                    ""id"": ""3544246b-afe7-4c12-8c42-6f8ec96913ce"",
                    ""path"": ""<Mouse>/scroll/y"",
                    ""interactions"": """",
                    ""processors"": ""Invert"",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""SelectWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""d8d8e497-99e0-42ef-bd96-cffcbad77f8b"",
                    ""path"": ""<Gamepad>/dpad/up"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SelectLastWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8979a3b1-1af9-4406-be5c-48a0fadf63a5"",
                    ""path"": ""<Keyboard>/q"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""SelectLastWeapon"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""39a56ac1-ea6c-4070-82c6-41e6349059ac"",
                    ""path"": ""<Gamepad>/leftStickPress"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""40a49d09-1bba-488b-8f3c-442d604c2e45"",
                    ""path"": ""<Keyboard>/ctrl"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Sneak"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eb059089-264c-4923-b829-0e2c8300adb4"",
                    ""path"": ""<Gamepad>/rightStickPress"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""SwitchView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""1dd79518-e234-4864-a0fd-5f00a84c2d58"",
                    ""path"": ""<Keyboard>/v"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""SwitchView"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""edfe1706-5437-49c0-ba35-bf8e5d248174"",
                    ""path"": ""<Gamepad>/buttonEast"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""09c6de74-fbe1-43d3-a19a-387279f08d2d"",
                    ""path"": ""<Keyboard>/r"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Reload"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""cb4a0e3e-ee09-48c8-9fd3-f0a438ddb758"",
                    ""path"": ""<Gamepad>/buttonWest"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8eaba957-a4be-4b45-b91a-89d420cb2bb5"",
                    ""path"": ""<Keyboard>/e"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Interact"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""6b7aa1a8-1efa-4efa-8bb0-982c181e9efa"",
                    ""path"": ""<Mouse>/rightButton"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Aim"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4131b796-8b77-4235-890c-3bfac51ff5e4"",
                    ""path"": ""<Gamepad>/start"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""36e0762d-0b11-4660-ad0c-303212b61e3c"",
                    ""path"": ""<Keyboard>/escape"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Pause"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""4b5f5959-f4a8-4dfe-9c9a-729b462fa4f7"",
                    ""path"": ""<Gamepad>/select"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Datapad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f502f1da-263d-4219-9bf1-9d8d95d6f292"",
                    ""path"": ""<Keyboard>/tab"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Datapad"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eee392d3-e2be-4e6c-b041-e08f1775fdaa"",
                    ""path"": ""<Gamepad>/rightTrigger"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Gamepad"",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""021e5ab4-6b01-4135-9efb-594bd99bb0b2"",
                    ""path"": ""<Mouse>/leftButton"",
                    ""interactions"": ""Press(behavior=2)"",
                    ""processors"": """",
                    ""groups"": ""Keyboard and mouse"",
                    ""action"": ""Hit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        },
        {
            ""name"": ""UsagesActionMap"",
            ""id"": ""8904d836-e732-437d-b0cf-76e079028efa"",
            ""actions"": [
                {
                    ""name"": ""Any"",
                    ""type"": ""Button"",
                    ""id"": ""eef00eeb-fecd-4a23-bbf3-430868c59805"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Back"",
                    ""type"": ""Button"",
                    ""id"": ""a040e0a7-226f-4dcb-85ee-a6631b6e1d0f"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Cancel"",
                    ""type"": ""Button"",
                    ""id"": ""faef346d-db26-4bfd-8d69-13f9b00ef68a"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Forward"",
                    ""type"": ""Button"",
                    ""id"": ""d000a919-9299-47c4-ba80-7317be07a555"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Menu"",
                    ""type"": ""Button"",
                    ""id"": ""7f4e5a99-39fd-4a6d-9150-e88e94b792b1"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Modifier"",
                    ""type"": ""Button"",
                    ""id"": ""9e6bad40-7a03-4826-bfc1-e3574a48d24b"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryAction"",
                    ""type"": ""Button"",
                    ""id"": ""877c1642-3302-4730-822e-37db43bdce18"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""PrimaryTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""e7c0ce1d-4bb8-4635-b866-db0e0e1f391c"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryAction"",
                    ""type"": ""Button"",
                    ""id"": ""590859ad-e9e5-4c61-b07e-d3f5de909c30"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""SecondaryTrigger"",
                    ""type"": ""Button"",
                    ""id"": ""dd21a5d2-f8d0-42e0-8910-0a9beef0dc0d"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                },
                {
                    ""name"": ""Submit"",
                    ""type"": ""Button"",
                    ""id"": ""0f6c08d5-c754-468d-9282-43c39bf42800"",
                    ""expectedControlType"": ""Button"",
                    ""processors"": """",
                    ""interactions"": """"
                }
            ],
            ""bindings"": [
                {
                    ""name"": """",
                    ""id"": ""b7275e78-7ec9-485a-b572-d881d2618cd6"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""12b06c27-f85e-4ed1-b1a7-407a7b96606b"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""8e29d98b-5214-4bf1-ae82-038d69a1fff5"",
                    ""path"": ""*/{Forward}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""978bcc85-db29-4832-9f13-d91d6656d2ef"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""68c9daa4-a581-459e-9ed1-fcf1c8c18af5"",
                    ""path"": ""*/{Modifier}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a8142cfe-b439-47a3-a2d9-3b775dbe5dc5"",
                    ""path"": ""*/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""fbb29cc4-7456-42ad-8be3-f88df60ac79e"",
                    ""path"": ""*/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""71a75c2b-9d42-4df8-b8a4-1142ddeb0436"",
                    ""path"": ""*/{SecondaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""a6d210d4-62ba-4d36-a812-646c98a95818"",
                    ""path"": ""*/{SecondaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""41267437-d00e-429e-b114-9162919fdc17"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Any"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""ad012863-03f1-48a8-8976-009c3f4864df"",
                    ""path"": ""*/{Cancel}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Cancel"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""691974f3-63c5-4f1f-99a1-732f1f3e4ae1"",
                    ""path"": ""*/{Forward}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Forward"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""eae9cb1e-6e78-4452-8e6d-e7021f150242"",
                    ""path"": ""*/{Menu}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Menu"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""c1e1569f-0f08-43f6-af77-bd3fc870e793"",
                    ""path"": ""*/{Modifier}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Modifier"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""377ad766-723b-4386-9650-33d876401844"",
                    ""path"": ""*/{PrimaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""f63a33f1-9372-4e96-bda3-f1c26822bd6f"",
                    ""path"": ""*/{PrimaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""PrimaryTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""45695053-27d7-4779-bf3a-74756036fe7d"",
                    ""path"": ""*/{SecondaryAction}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryAction"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""e32f5c82-7f16-470b-a602-fa2d3053dab0"",
                    ""path"": ""*/{SecondaryTrigger}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""SecondaryTrigger"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""746798d9-2472-477d-874e-b80e7a914b09"",
                    ""path"": ""*/{Submit}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Submit"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                },
                {
                    ""name"": """",
                    ""id"": ""b246dc3d-611b-45cc-a512-09edd7c177c3"",
                    ""path"": ""*/{Back}"",
                    ""interactions"": """",
                    ""processors"": """",
                    ""groups"": """",
                    ""action"": ""Back"",
                    ""isComposite"": false,
                    ""isPartOfComposite"": false
                }
            ]
        }
    ],
    ""controlSchemes"": [
        {
            ""name"": ""Gamepad"",
            ""bindingGroup"": ""Gamepad"",
            ""devices"": [
                {
                    ""devicePath"": ""<Gamepad>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        },
        {
            ""name"": ""Keyboard and mouse"",
            ""bindingGroup"": ""Keyboard and mouse"",
            ""devices"": [
                {
                    ""devicePath"": ""<Keyboard>"",
                    ""isOptional"": false,
                    ""isOR"": false
                },
                {
                    ""devicePath"": ""<Mouse>"",
                    ""isOptional"": false,
                    ""isOR"": false
                }
            ]
        }
    ]
}");
            // GameActionMap
            m_GameActionMap = asset.FindActionMap("GameActionMap", throwIfNotFound: true);
            m_GameActionMap_Aim = m_GameActionMap.FindAction("Aim", throwIfNotFound: true);
            m_GameActionMap_Datapad = m_GameActionMap.FindAction("Datapad", throwIfNotFound: true);
            m_GameActionMap_Hit = m_GameActionMap.FindAction("Hit", throwIfNotFound: true);
            m_GameActionMap_Interact = m_GameActionMap.FindAction("Interact", throwIfNotFound: true);
            m_GameActionMap_Jump = m_GameActionMap.FindAction("Jump", throwIfNotFound: true);
            m_GameActionMap_Look = m_GameActionMap.FindAction("Look", throwIfNotFound: true);
            m_GameActionMap_Move = m_GameActionMap.FindAction("Move", throwIfNotFound: true);
            m_GameActionMap_Pause = m_GameActionMap.FindAction("Pause", throwIfNotFound: true);
            m_GameActionMap_Reload = m_GameActionMap.FindAction("Reload", throwIfNotFound: true);
            m_GameActionMap_SelectLastWeapon = m_GameActionMap.FindAction("SelectLastWeapon", throwIfNotFound: true);
            m_GameActionMap_SelectWeapon = m_GameActionMap.FindAction("SelectWeapon", throwIfNotFound: true);
            m_GameActionMap_Sneak = m_GameActionMap.FindAction("Sneak", throwIfNotFound: true);
            m_GameActionMap_Sprint = m_GameActionMap.FindAction("Sprint", throwIfNotFound: true);
            m_GameActionMap_SwitchView = m_GameActionMap.FindAction("SwitchView", throwIfNotFound: true);
            // UsagesActionMap
            m_UsagesActionMap = asset.FindActionMap("UsagesActionMap", throwIfNotFound: true);
            m_UsagesActionMap_Any = m_UsagesActionMap.FindAction("Any", throwIfNotFound: true);
            m_UsagesActionMap_Back = m_UsagesActionMap.FindAction("Back", throwIfNotFound: true);
            m_UsagesActionMap_Cancel = m_UsagesActionMap.FindAction("Cancel", throwIfNotFound: true);
            m_UsagesActionMap_Forward = m_UsagesActionMap.FindAction("Forward", throwIfNotFound: true);
            m_UsagesActionMap_Menu = m_UsagesActionMap.FindAction("Menu", throwIfNotFound: true);
            m_UsagesActionMap_Modifier = m_UsagesActionMap.FindAction("Modifier", throwIfNotFound: true);
            m_UsagesActionMap_PrimaryAction = m_UsagesActionMap.FindAction("PrimaryAction", throwIfNotFound: true);
            m_UsagesActionMap_PrimaryTrigger = m_UsagesActionMap.FindAction("PrimaryTrigger", throwIfNotFound: true);
            m_UsagesActionMap_SecondaryAction = m_UsagesActionMap.FindAction("SecondaryAction", throwIfNotFound: true);
            m_UsagesActionMap_SecondaryTrigger = m_UsagesActionMap.FindAction("SecondaryTrigger", throwIfNotFound: true);
            m_UsagesActionMap_Submit = m_UsagesActionMap.FindAction("Submit", throwIfNotFound: true);
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

        // GameActionMap
        private readonly InputActionMap m_GameActionMap;
        private IGameActionMapActions m_GameActionMapActionsCallbackInterface;
        private readonly InputAction m_GameActionMap_Aim;
        private readonly InputAction m_GameActionMap_Datapad;
        private readonly InputAction m_GameActionMap_Hit;
        private readonly InputAction m_GameActionMap_Interact;
        private readonly InputAction m_GameActionMap_Jump;
        private readonly InputAction m_GameActionMap_Look;
        private readonly InputAction m_GameActionMap_Move;
        private readonly InputAction m_GameActionMap_Pause;
        private readonly InputAction m_GameActionMap_Reload;
        private readonly InputAction m_GameActionMap_SelectLastWeapon;
        private readonly InputAction m_GameActionMap_SelectWeapon;
        private readonly InputAction m_GameActionMap_Sneak;
        private readonly InputAction m_GameActionMap_Sprint;
        private readonly InputAction m_GameActionMap_SwitchView;
        public struct GameActionMapActions
        {
            private @GameInputActions m_Wrapper;
            public GameActionMapActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Aim => m_Wrapper.m_GameActionMap_Aim;
            public InputAction @Datapad => m_Wrapper.m_GameActionMap_Datapad;
            public InputAction @Hit => m_Wrapper.m_GameActionMap_Hit;
            public InputAction @Interact => m_Wrapper.m_GameActionMap_Interact;
            public InputAction @Jump => m_Wrapper.m_GameActionMap_Jump;
            public InputAction @Look => m_Wrapper.m_GameActionMap_Look;
            public InputAction @Move => m_Wrapper.m_GameActionMap_Move;
            public InputAction @Pause => m_Wrapper.m_GameActionMap_Pause;
            public InputAction @Reload => m_Wrapper.m_GameActionMap_Reload;
            public InputAction @SelectLastWeapon => m_Wrapper.m_GameActionMap_SelectLastWeapon;
            public InputAction @SelectWeapon => m_Wrapper.m_GameActionMap_SelectWeapon;
            public InputAction @Sneak => m_Wrapper.m_GameActionMap_Sneak;
            public InputAction @Sprint => m_Wrapper.m_GameActionMap_Sprint;
            public InputAction @SwitchView => m_Wrapper.m_GameActionMap_SwitchView;
            public InputActionMap Get() { return m_Wrapper.m_GameActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(GameActionMapActions set) { return set.Get(); }
            public void SetCallbacks(IGameActionMapActions instance)
            {
                if (m_Wrapper.m_GameActionMapActionsCallbackInterface != null)
                {
                    @Aim.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnAim;
                    @Aim.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnAim;
                    @Aim.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnAim;
                    @Datapad.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnDatapad;
                    @Datapad.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnDatapad;
                    @Datapad.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnDatapad;
                    @Hit.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnHit;
                    @Hit.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnHit;
                    @Hit.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnHit;
                    @Interact.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnInteract;
                    @Interact.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnInteract;
                    @Interact.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnInteract;
                    @Jump.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnJump;
                    @Jump.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnJump;
                    @Jump.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnJump;
                    @Look.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnLook;
                    @Look.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnLook;
                    @Look.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnLook;
                    @Move.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnMove;
                    @Move.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnMove;
                    @Move.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnMove;
                    @Pause.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnPause;
                    @Pause.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnPause;
                    @Pause.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnPause;
                    @Reload.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnReload;
                    @Reload.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnReload;
                    @Reload.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnReload;
                    @SelectLastWeapon.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectLastWeapon;
                    @SelectLastWeapon.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectLastWeapon;
                    @SelectLastWeapon.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectLastWeapon;
                    @SelectWeapon.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectWeapon;
                    @SelectWeapon.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectWeapon;
                    @SelectWeapon.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSelectWeapon;
                    @Sneak.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSneak;
                    @Sneak.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSneak;
                    @Sneak.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSneak;
                    @Sprint.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSprint;
                    @Sprint.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSprint;
                    @Sprint.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSprint;
                    @SwitchView.started -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSwitchView;
                    @SwitchView.performed -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSwitchView;
                    @SwitchView.canceled -= m_Wrapper.m_GameActionMapActionsCallbackInterface.OnSwitchView;
                }
                m_Wrapper.m_GameActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Aim.started += instance.OnAim;
                    @Aim.performed += instance.OnAim;
                    @Aim.canceled += instance.OnAim;
                    @Datapad.started += instance.OnDatapad;
                    @Datapad.performed += instance.OnDatapad;
                    @Datapad.canceled += instance.OnDatapad;
                    @Hit.started += instance.OnHit;
                    @Hit.performed += instance.OnHit;
                    @Hit.canceled += instance.OnHit;
                    @Interact.started += instance.OnInteract;
                    @Interact.performed += instance.OnInteract;
                    @Interact.canceled += instance.OnInteract;
                    @Jump.started += instance.OnJump;
                    @Jump.performed += instance.OnJump;
                    @Jump.canceled += instance.OnJump;
                    @Look.started += instance.OnLook;
                    @Look.performed += instance.OnLook;
                    @Look.canceled += instance.OnLook;
                    @Move.started += instance.OnMove;
                    @Move.performed += instance.OnMove;
                    @Move.canceled += instance.OnMove;
                    @Pause.started += instance.OnPause;
                    @Pause.performed += instance.OnPause;
                    @Pause.canceled += instance.OnPause;
                    @Reload.started += instance.OnReload;
                    @Reload.performed += instance.OnReload;
                    @Reload.canceled += instance.OnReload;
                    @SelectLastWeapon.started += instance.OnSelectLastWeapon;
                    @SelectLastWeapon.performed += instance.OnSelectLastWeapon;
                    @SelectLastWeapon.canceled += instance.OnSelectLastWeapon;
                    @SelectWeapon.started += instance.OnSelectWeapon;
                    @SelectWeapon.performed += instance.OnSelectWeapon;
                    @SelectWeapon.canceled += instance.OnSelectWeapon;
                    @Sneak.started += instance.OnSneak;
                    @Sneak.performed += instance.OnSneak;
                    @Sneak.canceled += instance.OnSneak;
                    @Sprint.started += instance.OnSprint;
                    @Sprint.performed += instance.OnSprint;
                    @Sprint.canceled += instance.OnSprint;
                    @SwitchView.started += instance.OnSwitchView;
                    @SwitchView.performed += instance.OnSwitchView;
                    @SwitchView.canceled += instance.OnSwitchView;
                }
            }
        }
        public GameActionMapActions @GameActionMap => new GameActionMapActions(this);

        // UsagesActionMap
        private readonly InputActionMap m_UsagesActionMap;
        private IUsagesActionMapActions m_UsagesActionMapActionsCallbackInterface;
        private readonly InputAction m_UsagesActionMap_Any;
        private readonly InputAction m_UsagesActionMap_Back;
        private readonly InputAction m_UsagesActionMap_Cancel;
        private readonly InputAction m_UsagesActionMap_Forward;
        private readonly InputAction m_UsagesActionMap_Menu;
        private readonly InputAction m_UsagesActionMap_Modifier;
        private readonly InputAction m_UsagesActionMap_PrimaryAction;
        private readonly InputAction m_UsagesActionMap_PrimaryTrigger;
        private readonly InputAction m_UsagesActionMap_SecondaryAction;
        private readonly InputAction m_UsagesActionMap_SecondaryTrigger;
        private readonly InputAction m_UsagesActionMap_Submit;
        public struct UsagesActionMapActions
        {
            private @GameInputActions m_Wrapper;
            public UsagesActionMapActions(@GameInputActions wrapper) { m_Wrapper = wrapper; }
            public InputAction @Any => m_Wrapper.m_UsagesActionMap_Any;
            public InputAction @Back => m_Wrapper.m_UsagesActionMap_Back;
            public InputAction @Cancel => m_Wrapper.m_UsagesActionMap_Cancel;
            public InputAction @Forward => m_Wrapper.m_UsagesActionMap_Forward;
            public InputAction @Menu => m_Wrapper.m_UsagesActionMap_Menu;
            public InputAction @Modifier => m_Wrapper.m_UsagesActionMap_Modifier;
            public InputAction @PrimaryAction => m_Wrapper.m_UsagesActionMap_PrimaryAction;
            public InputAction @PrimaryTrigger => m_Wrapper.m_UsagesActionMap_PrimaryTrigger;
            public InputAction @SecondaryAction => m_Wrapper.m_UsagesActionMap_SecondaryAction;
            public InputAction @SecondaryTrigger => m_Wrapper.m_UsagesActionMap_SecondaryTrigger;
            public InputAction @Submit => m_Wrapper.m_UsagesActionMap_Submit;
            public InputActionMap Get() { return m_Wrapper.m_UsagesActionMap; }
            public void Enable() { Get().Enable(); }
            public void Disable() { Get().Disable(); }
            public bool enabled => Get().enabled;
            public static implicit operator InputActionMap(UsagesActionMapActions set) { return set.Get(); }
            public void SetCallbacks(IUsagesActionMapActions instance)
            {
                if (m_Wrapper.m_UsagesActionMapActionsCallbackInterface != null)
                {
                    @Any.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnAny;
                    @Any.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnAny;
                    @Any.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnAny;
                    @Back.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnBack;
                    @Back.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnBack;
                    @Back.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnBack;
                    @Cancel.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnCancel;
                    @Cancel.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnCancel;
                    @Cancel.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnCancel;
                    @Forward.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnForward;
                    @Forward.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnForward;
                    @Forward.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnForward;
                    @Menu.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnMenu;
                    @Menu.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnMenu;
                    @Menu.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnMenu;
                    @Modifier.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnModifier;
                    @Modifier.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnModifier;
                    @Modifier.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnModifier;
                    @PrimaryAction.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryAction;
                    @PrimaryAction.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryAction;
                    @PrimaryAction.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryAction;
                    @PrimaryTrigger.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryTrigger;
                    @PrimaryTrigger.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryTrigger;
                    @PrimaryTrigger.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnPrimaryTrigger;
                    @SecondaryAction.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryAction;
                    @SecondaryAction.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryAction;
                    @SecondaryAction.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryAction;
                    @SecondaryTrigger.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryTrigger;
                    @SecondaryTrigger.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryTrigger;
                    @SecondaryTrigger.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSecondaryTrigger;
                    @Submit.started -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSubmit;
                    @Submit.performed -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSubmit;
                    @Submit.canceled -= m_Wrapper.m_UsagesActionMapActionsCallbackInterface.OnSubmit;
                }
                m_Wrapper.m_UsagesActionMapActionsCallbackInterface = instance;
                if (instance != null)
                {
                    @Any.started += instance.OnAny;
                    @Any.performed += instance.OnAny;
                    @Any.canceled += instance.OnAny;
                    @Back.started += instance.OnBack;
                    @Back.performed += instance.OnBack;
                    @Back.canceled += instance.OnBack;
                    @Cancel.started += instance.OnCancel;
                    @Cancel.performed += instance.OnCancel;
                    @Cancel.canceled += instance.OnCancel;
                    @Forward.started += instance.OnForward;
                    @Forward.performed += instance.OnForward;
                    @Forward.canceled += instance.OnForward;
                    @Menu.started += instance.OnMenu;
                    @Menu.performed += instance.OnMenu;
                    @Menu.canceled += instance.OnMenu;
                    @Modifier.started += instance.OnModifier;
                    @Modifier.performed += instance.OnModifier;
                    @Modifier.canceled += instance.OnModifier;
                    @PrimaryAction.started += instance.OnPrimaryAction;
                    @PrimaryAction.performed += instance.OnPrimaryAction;
                    @PrimaryAction.canceled += instance.OnPrimaryAction;
                    @PrimaryTrigger.started += instance.OnPrimaryTrigger;
                    @PrimaryTrigger.performed += instance.OnPrimaryTrigger;
                    @PrimaryTrigger.canceled += instance.OnPrimaryTrigger;
                    @SecondaryAction.started += instance.OnSecondaryAction;
                    @SecondaryAction.performed += instance.OnSecondaryAction;
                    @SecondaryAction.canceled += instance.OnSecondaryAction;
                    @SecondaryTrigger.started += instance.OnSecondaryTrigger;
                    @SecondaryTrigger.performed += instance.OnSecondaryTrigger;
                    @SecondaryTrigger.canceled += instance.OnSecondaryTrigger;
                    @Submit.started += instance.OnSubmit;
                    @Submit.performed += instance.OnSubmit;
                    @Submit.canceled += instance.OnSubmit;
                }
            }
        }
        public UsagesActionMapActions @UsagesActionMap => new UsagesActionMapActions(this);
        private int m_GamepadSchemeIndex = -1;
        public InputControlScheme GamepadScheme
        {
            get
            {
                if (m_GamepadSchemeIndex == -1) m_GamepadSchemeIndex = asset.FindControlSchemeIndex("Gamepad");
                return asset.controlSchemes[m_GamepadSchemeIndex];
            }
        }
        private int m_KeyboardandmouseSchemeIndex = -1;
        public InputControlScheme KeyboardandmouseScheme
        {
            get
            {
                if (m_KeyboardandmouseSchemeIndex == -1) m_KeyboardandmouseSchemeIndex = asset.FindControlSchemeIndex("Keyboard and mouse");
                return asset.controlSchemes[m_KeyboardandmouseSchemeIndex];
            }
        }
        public interface IGameActionMapActions
        {
            void OnAim(InputAction.CallbackContext context);
            void OnDatapad(InputAction.CallbackContext context);
            void OnHit(InputAction.CallbackContext context);
            void OnInteract(InputAction.CallbackContext context);
            void OnJump(InputAction.CallbackContext context);
            void OnLook(InputAction.CallbackContext context);
            void OnMove(InputAction.CallbackContext context);
            void OnPause(InputAction.CallbackContext context);
            void OnReload(InputAction.CallbackContext context);
            void OnSelectLastWeapon(InputAction.CallbackContext context);
            void OnSelectWeapon(InputAction.CallbackContext context);
            void OnSneak(InputAction.CallbackContext context);
            void OnSprint(InputAction.CallbackContext context);
            void OnSwitchView(InputAction.CallbackContext context);
        }
        public interface IUsagesActionMapActions
        {
            void OnAny(InputAction.CallbackContext context);
            void OnBack(InputAction.CallbackContext context);
            void OnCancel(InputAction.CallbackContext context);
            void OnForward(InputAction.CallbackContext context);
            void OnMenu(InputAction.CallbackContext context);
            void OnModifier(InputAction.CallbackContext context);
            void OnPrimaryAction(InputAction.CallbackContext context);
            void OnPrimaryTrigger(InputAction.CallbackContext context);
            void OnSecondaryAction(InputAction.CallbackContext context);
            void OnSecondaryTrigger(InputAction.CallbackContext context);
            void OnSubmit(InputAction.CallbackContext context);
        }
    }
}
