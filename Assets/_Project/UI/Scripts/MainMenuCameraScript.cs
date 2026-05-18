using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuCameraScript : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private GameObject sun;
    [SerializeField] private Vector3 offset;

    // Start is called before the first frame update
    void Update()
    {
        Cursor.lockState = CursorLockMode.Confined;
        Vector3 lookAt = new Vector3(sun.transform.position.x + offset.x,
            sun.transform.position.y + offset.y,
            sun.transform.position.z + offset.z);

        //transform.LookAt(lookAt);// used for centering the camera on the sun during development
    }
}
