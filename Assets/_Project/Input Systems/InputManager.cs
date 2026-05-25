using UnityEngine.InputSystem;
using UnityEngine;

public class InputManager : MonoBehaviour
{
    public static InputSystem CONTROLS;

    //base actions
    public static InputAction MOVEACTION;
    public static InputAction INTERACTACTION;
    public static InputAction LOOKACTION;

    //ui actions
    public static InputAction UICLICKACTION;
    public static InputAction UIMOUSEPOS;
    public static InputAction UIBACK;
    public static InputAction UIMOUSEPOSDELTA;

    private void Awake()
    {
        CONTROLS = new InputSystem();

        //base map
        MOVEACTION = CONTROLS.Base.Move;
        INTERACTACTION = CONTROLS.Base.Interact;
        LOOKACTION = CONTROLS.Base.Look;

        //ui map
        UICLICKACTION = CONTROLS.UI.Click;
        UIMOUSEPOS = CONTROLS.UI.PointerPosition;
        UIBACK = CONTROLS.UI.Back;
        UIMOUSEPOSDELTA = CONTROLS.UI.DeltaPointerPos;
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