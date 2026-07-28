using TMPro.Examples;
using UnityEngine;

//DO NOT ATTACH THIS SCRIPT TO A GAMEOBJECT!

public static class GameManager
{
    public static PM_MovementModes CURRENTMODE { get; set; } 
    public static PM_PlayerMovement PLAYER_MOVEMENT { get; set; } 
    public static PM_CameraController CAMERA_CONTROLLER { get; set; } 

    public static float GM_CAMERA_WIDTH_PX { get; set; }
    public static float GM_CAMERA_HEIGHT_PX { get; set; }

    public static float UI_SMOOTH_SPEED = 3f;

    public static bool CUTSCENE;

    public static void SETCUTSCENE(bool cutSceneStatus)
    {
        CUTSCENE = cutSceneStatus;
        if(cutSceneStatus) {
            PLAYER_MOVEMENT.SetInputLock(true);
            CAMERA_CONTROLLER.SetInputLock(true);
        } else
        {
            PLAYER_MOVEMENT.SetInputLock(false);
            CAMERA_CONTROLLER.SetInputLock(false);
        }
    }
}
