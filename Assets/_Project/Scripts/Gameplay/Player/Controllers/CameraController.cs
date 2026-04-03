using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;

public class CameraController : MonoBehaviour
{
    [Header("Inputs")]
    public bool lockCamera = false;
    [SerializeField] private InputSystem controls; //asset
    [SerializeField] private InputAction lookAction;

    [Header("Functionality")]
    [SerializeField] private float upperLookLimit = -80f;
    [SerializeField] private float lowerLookLimit = 70f;

    [Header("Customizability")]
    [SerializeField] private float xSens = 100f;
    [SerializeField] private float ySens = 100f;

    [SerializeField] private float xRot = 0f;
    [SerializeField] private Transform playerTransform;

    private void Awake()
    {
        controls = new InputSystem();
        lookAction = controls.Base.Look;
    }

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        if (!lockCamera)
        {
            //get value
            Vector2 lookInput = lookAction.ReadValue<Vector2>();

            //multiply by sensitivity and time.deltaTime for speed and framerate adjustment
            float mouseX = lookInput.x * xSens * Time.deltaTime;
            float mouseY = lookInput.y * ySens * Time.deltaTime;

            //rotate
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, upperLookLimit, lowerLookLimit);
            transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);

            //rotate player body
            playerTransform.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // allows free movement
            Cursor.visible = true;                 // shows the cursor
        }
    }

    private void OnEnable()
    {
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

}