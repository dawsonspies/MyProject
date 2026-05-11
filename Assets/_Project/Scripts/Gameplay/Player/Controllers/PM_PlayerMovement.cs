using UnityEngine;
using static GameManager;
using UnityEngine.InputSystem;

public enum PM_MovementModes
{
    Shaky,
    Normal,
    ZeroGrav, //Only allow movements in directions which are a cirtant distance from an object with a collider,
              //bean character is now nonexistant and the camera can look wherever.
    Disabled,
    Idle
}

[RequireComponent(typeof(CharacterController))]
public class PM_PlayerMovement : MonoBehaviour
{
    [Header("MovementModes")]
    [SerializeField] private bool shakeMode = true;

    [Header("Movement Speeds")]
    [SerializeField] private float currentSpeed;
    [SerializeField] private float normalSpeed = 2.5f;
    [SerializeField] private float floatSpeed = 2.5f;
    [SerializeField] private float shakyBaseSpeed = 1.5f;
    [SerializeField] private float shakyPercent = 0f;

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

    public float GetShakyPercent()
    {
        return shakyPercent;
    }

    public void SetMovementMode(PM_MovementModes new_mode, float shaky_percent = 25f)
    {
        CURRENTMODE = new_mode;

        if(CURRENTMODE == PM_MovementModes.Shaky)
            shakyPercent = shaky_percent;
    }

    private void Update()
    {
        if (CURRENTMODE != PM_MovementModes.Disabled)
        {
            Vector2 moveInput = InputManager.MOVEACTION.ReadValue<Vector2>();
            bool interactPressed = InputManager.INTERACTACTION.WasPressedThisFrame();
            Vector2 lookInput = InputManager.LOOKACTION.ReadValue<Vector2>();

            ResolveState(moveInput);

            Vector3 move = CalculateMovement(moveInput);
            Vector3 finalMove = move + verticalVelocity;

            charCont.Move(finalMove * Time.deltaTime);
        }

        print(CURRENTMODE);

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

/*        if (idk case)
        {
            CURRENTMODE = PM_MovementModes.ZeroGrav;
            return;
        }*/

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
            speed = baseSpeed * (0.5f - shakyPercent / 100f + limpOscillation);

            float swayAngle = Mathf.Sin(Time.time * 2f) * (shakyPercent / 100f) * 10f;
            forward = Quaternion.Euler(0f, swayAngle, 0f) * forward;

            float verticalBob = Mathf.Sin(Time.time * 3f) * 0.05f;
            Vector3 move = (forward * input.y + right * input.x).normalized;
            move.y += verticalBob;
            move *= -speed;
            return move;
        }

        return (forward * input.y + right * input.x) * normalSpeed;
    }
}
