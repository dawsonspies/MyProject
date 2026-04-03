using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Input")]
    public bool lockControls = false;
    private InputSystem controls;
    private InputAction moveAction;
    private InputAction crouchAction;
    private InputAction jumpAction;

    [Header("Movement Speeds")]
    [SerializeField] private float walkSpeed = 2.5f;
    [SerializeField] private float crouchSpeed = 1.2f;

    [Header("Gravity / Jump")]
    [SerializeField] private float gravity = -20f;
    [SerializeField] private float jumpForce = 8f;

    [Header("Controller Heights")]
    [SerializeField] private float standingHeight = 1.0f;
    [SerializeField] private float crouchHeight = 0.5f;

    [Header("References")]
    [SerializeField] private Transform cameraTransform;

    private CharacterController controller;
    private Vector3 verticalVelocity;

    private enum MovementState
    {
        Idle,
        Walk,
        Crouch
    }

    private MovementState currentState = MovementState.Idle;

    private void Awake()
    {
        controls = new InputSystem();

        moveAction = controls.Base.Move;
        crouchAction = controls.Base.Crouch;
        jumpAction = controls.Base.Jump;

        controller = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        controls.Enable();
    }

    private void OnDisable()
    {
        controls.Disable();
    }

    private void Update()
    {
        if (!lockControls)
        {
            Vector2 moveInput = moveAction.ReadValue<Vector2>();
            bool isCrouching = crouchAction.IsPressed();
            bool jumpPressed = jumpAction.WasPressedThisFrame();

            ResolveState(moveInput, isCrouching);

            ApplyStatePhysicals();

            Vector3 move = CalculateMovement(moveInput);

            HandleVerticalMovement(jumpPressed);

            Vector3 finalMove = move + verticalVelocity;
            controller.Move(finalMove * Time.deltaTime);
        }
    }

    private void ResolveState(Vector2 moveInput, bool crouch)
    {
        bool grounded = controller.isGrounded;

        if (crouch)
        {
            currentState = MovementState.Crouch;
            return;
        }

        if (moveInput == Vector2.zero)
        {
            currentState = MovementState.Idle;
            return;
        }

        if (!grounded)
            return;

        currentState = MovementState.Walk;
    }

    private void ApplyStatePhysicals()
    {
        switch (currentState)
        {
            case MovementState.Crouch:
                transform.localScale = new Vector3(1, crouchHeight, 1);
                break;

            default:
                transform.localScale = new Vector3(1, standingHeight, 1);
                break;
        }
    }

    private Vector3 CalculateMovement(Vector2 input)
    {
        float speed = currentState switch
        {
            MovementState.Crouch => crouchSpeed,
            MovementState.Walk => walkSpeed,
            _ => 0f
        };

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        return (forward * input.y + right * input.x) * speed;
    }

    private void HandleVerticalMovement(bool jumpPressed)
    {
        if (controller.isGrounded)
        {
            if (verticalVelocity.y < 0f)
                verticalVelocity.y = -2f;

            if (jumpPressed)
                verticalVelocity.y = jumpForce;
        }

        verticalVelocity.y += gravity * Time.deltaTime;
    }
}