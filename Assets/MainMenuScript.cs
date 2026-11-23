using UnityEngine;
using UnityEngine.InputSystem;

public class MainMenuScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject outputLine;
    [SerializeField] private GameObject inputLinePrefab;
    [SerializeField] private GameObject inputLine;
    [SerializeField] private Transform linesHolder;

    private void Update()
    {
        if (Keyboard.current == null) return;

        // Triggers once on the frame the key is pressed down
        if (Keyboard.current.enterKey.wasPressedThisFrame)
        {
            Destroy(inputLine.gameObject);
            Instantiate(outputLine, linesHolder);
            inputLine = Instantiate(inputLinePrefab, linesHolder);
        }
    }
}
