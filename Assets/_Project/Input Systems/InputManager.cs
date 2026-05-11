using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputSystem CONTROLS;
    public static InputAction MOVEACTION;
    public static InputAction INTERACTACTION;
    public static InputAction LOOKACTION;

    private void Awake()
    {
        CONTROLS = new InputSystem();
        MOVEACTION = CONTROLS.Base.Move;
        INTERACTACTION = CONTROLS.Base.Interact;
        LOOKACTION = CONTROLS.Base.Look;
    }

    private void OnEnable()
    {
        CONTROLS.Enable();
    }

    private void OnDisable()
    {
        CONTROLS.Disable();
    }
}