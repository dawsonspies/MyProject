using UnityEngine;
using static GameManager;

public class MainMenuUIScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private StarSpawner starSpawnerScript;

    private void Start()
    {
        //set global vars
        GM_CAMERA_HEIGHT_PX = Screen.height;
        GM_CAMERA_WIDTH_PX = Screen.width;

        //set local references
        starSpawnerScript = GetComponent<StarSpawner>();

        //call local references
        starSpawnerScript.SpawnStars();
    }

    private void Update()
    {
        if (InputManager.UIBACK.WasPressedThisFrame())
        {
            //go from settings to main menu
        }
    }
}
