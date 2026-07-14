using static GameManager;
using UnityEngine;

public class PM_CameraController : MonoBehaviour
{
    [Header("Inputs")]
    public bool lockCamera = false;
    public bool shakeyMode = false;

    [Header("Functionality")]
    [SerializeField] private float upperLookLimit = -80f;
    [SerializeField] private float lowerLookLimit = 70f;

    [Header("Customizability")]
    [SerializeField] private float xSens = 100f;
    [SerializeField] private float ySens = 100f;
    [SerializeField] private float xRot = 0f;
    [SerializeField] private Transform playerTransform;
    [SerializeField] private PM_PlayerMovement pm;
    [SerializeField] private float shakeyPercent;

    public void SetLookLimits(float _upperLookLimit = Mathf.Infinity, float _lowerLookLimit = Mathf.Infinity)
    {
        if (_upperLookLimit != Mathf.Infinity)
            upperLookLimit = _upperLookLimit;
        if (_lowerLookLimit != Mathf.Infinity)
            lowerLookLimit = _lowerLookLimit;
    }

    public void SetShakey(float _shakeyPercent)
    {
        shakeyPercent = _shakeyPercent;
    }

    private void Awake()
    {
        pm = playerTransform.GetComponent<PM_PlayerMovement>();
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
            Vector2 lookInput = InputManager.LOOKACTION.ReadValue<Vector2>();

            //multiply by sensitivity and time.deltaTime for speed and framerate adjustment
            float mouseX = lookInput.x * xSens * Time.deltaTime;
            float mouseY = lookInput.y * ySens * Time.deltaTime;

            //rotate
            xRot -= mouseY;
            xRot = Mathf.Clamp(xRot, upperLookLimit, lowerLookLimit);

            //random
            float tiltX;
            float tiltY;
            float tiltZ;

            if (shakeyMode)
            {
                tiltX = Mathf.Sin(Time.time * 1.5f) * (shakeyPercent / 100f) * 5f; // pitch wobble
                tiltY = Mathf.Sin(Time.time * 2f) * (shakeyPercent / 100f) * 5f; // yaw wobble
                tiltZ = Mathf.Sin(Time.time * 0.5f) * (shakeyPercent / 100f) * 5f; // roll wobble
                transform.localRotation = Quaternion.Euler(xRot + tiltX, tiltY, tiltZ);
            } else
            {
                transform.localRotation = Quaternion.Euler(xRot, 0f, 0f);
            }

            //rotate player body
            playerTransform.Rotate(Vector3.up * mouseX);
        }
        else
        {
            Cursor.lockState = CursorLockMode.None; // allows free movement
            Cursor.visible = true;                 // shows the cursor
        }
    }
}
