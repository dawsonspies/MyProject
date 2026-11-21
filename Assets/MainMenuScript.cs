using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private MainMenuUICursor cursor;

    private void Update()
    {
        if (Keyboard.current == null) return;

        // Triggers once on the frame the key is pressed down
        if (Keyboard.current.downArrowKey.wasPressedThisFrame || Keyboard.current.sKey.wasPressedThisFrame)
        {
            cursor.ChangeIndex(-1);
        }
        else if (Keyboard.current.upArrowKey.wasPressedThisFrame || Keyboard.current.wKey.wasPressedThisFrame)
        {
            cursor.ChangeIndex(1);
        }
    }
}
