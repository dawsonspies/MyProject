using UnityEngine;
using static GameManager;

public enum PM_MovementModes
{
    Shaky,
    Normal,
    Disabled,
    Idle
}

[RequireComponent(typeof(CharacterController))]
public class PM_PlayerMovement : MonoBehaviour
{
    [Header("MovementModes")]
    [SerializeField] private bool shakeMode = true;
    [SerializeField] private PM_MovementModes currentMode = CURRENTMODE;

    [Header("Movement Speeds")]
    [SerializeField] private float currentSpeed; //pretty much for display only
    [SerializeField] private float normalSpeed = 2.5f;
    [SerializeField] private float shakyBaseSpeed = 1.5f;
    [SerializeField] private float shakeyPercent = 0f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private PlayerInteraction playerInteractionScript;
    [SerializeField] private CharacterController charCont;

    private Vector3 verticalVelocity;

    private void Awake()
    {
        playerInteractionScript = GetComponent<PlayerInteraction>();

        charCont = GetComponent<CharacterController>();
    }

    public void SetInputLock(bool _locked)
    {
        if (_locked)
            CURRENTMODE = PM_MovementModes.Disabled;
        else
            CURRENTMODE = PM_MovementModes.Normal;
    }

    public void SetShakey(float _newShakey)
    {
        shakeyPercent = _newShakey;
    }

    public void SetMovementMode(PM_MovementModes new_mode, float shaky_percent = 25f)
    {
        CURRENTMODE = new_mode;

        if(CURRENTMODE == PM_MovementModes.Shaky)
            shakeyPercent = shaky_percent;
    }

    private void Update()
    {
        if (CURRENTMODE != PM_MovementModes.Disabled)
        {
            Vector2 moveInput = InputManager.MOVEACTION.ReadValue<Vector2>();
            bool interactPressed = InputManager.INTERACTACTION.WasPressedThisFrame();

            ResolveState(moveInput);

            Vector3 move = CalculateMovement(moveInput);
            Vector3 finalMove = move + verticalVelocity;

            charCont.Move(finalMove * Time.deltaTime);
        }

        currentSpeed = charCont.velocity.magnitude;
    }

    private void ResolveState(Vector2 moveInput)
    {
        if (shakeMode)
        {
            CURRENTMODE = PM_MovementModes.Shaky;
            return;
        }

        bool grounded = charCont.isGrounded;

        if (moveInput == Vector2.zero)
        {
            CURRENTMODE = PM_MovementModes.Idle;
            return;
        }

        CURRENTMODE = PM_MovementModes.Normal;
    }

    private Vector3 CalculateMovement(Vector2 input)
    {
        // Flattened forward/right so player movement aligns with camera but stays horizontal
        Vector3 forward = cameraTransform.forward;
        forward.y = 0f;
        forward.Normalize();

        Vector3 right = cameraTransform.right;
        right.y = 0f;
        right.Normalize();

        float speed;

        // Shaky mode handled separately
        if (CURRENTMODE == PM_MovementModes.Shaky)
        {
            float baseSpeed = shakyBaseSpeed;
            float limpOscillation = Mathf.Sin(Time.time * 1.5f) * 0.3f;
            speed = baseSpeed * (0.5f - shakeyPercent / 100f + limpOscillation);

            float swayAngle = Mathf.Sin(Time.time * 2f) * (shakeyPercent / 100f) * 10f;
            forward = Quaternion.Euler(0f, swayAngle, 0f) * forward;

            float verticalBob = Mathf.Sin(Time.time * 3f) * 0.05f;
            Vector3 move = (forward * input.y + right * input.x).normalized;
            move.y += verticalBob;
            move *= speed;
            return move;
        }

        return (forward * input.y + right * input.x) * normalSpeed;
    }
}
