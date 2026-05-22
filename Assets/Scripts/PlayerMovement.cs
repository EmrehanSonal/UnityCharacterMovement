using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private GroundCheck groundCheck;
    private Rigidbody chRigidbody;

    private bool isSprinting;
    [SerializeField] private float walkSpeed = 5f;
    [SerializeField] private float sprintSpeed = 9f;

    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private float standingHeight = 2f;
    [SerializeField] private float crouchHeight = 1f;
    [SerializeField] private float crouchSpeed = 3f;
    private bool isCrouching;

    private void Awake()
    {
        chRigidbody = GetComponent<Rigidbody>();
        if (groundCheck == null)
        {
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        // try to auto-assign the capsule collider if not set in inspector
        if (capsuleCollider == null)
        {
            capsuleCollider = GetComponent<CapsuleCollider>() ?? GetComponentInChildren<CapsuleCollider>();
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // Sprinting - use named handlers so we can unsubscribe cleanly
        playerInputActions.Player.Sprint.performed += OnSprintPerformed;
        playerInputActions.Player.Sprint.canceled += OnSprintCanceled;

        playerInputActions.Player.Crouch.performed += OnCrouchPerformed;
        playerInputActions.Player.Crouch.canceled += OnCrouchCanceled;
    }

    private void OnSprintPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        isSprinting = true;
    }

    private void OnSprintCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        isSprinting = false;
    }

    private void OnCrouchPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StartCrouch();
    }

    private void OnCrouchCanceled(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        StopCrouch();
    }

    public Vector2 GetMovementVectorNormalized()
    {   //wasd control
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();
        inputVector = inputVector.normalized;
        return inputVector;
    }
    
    public void Jump()
    {
        if (groundCheck == null || !groundCheck.IsGrounded())
        {
            return;    
        }
        chRigidbody.AddForce(Vector3.up * 5f, ForceMode.Impulse);
    }

    public float GetCurrentSpeed()
    {  
        if (isCrouching)
        {
            return crouchSpeed;
        }
         //if isSprinting is true  returns sprintSpeed, if false return walkSpeed. 
        return isSprinting ? sprintSpeed : walkSpeed;
    }

    public void StartCrouch()
    {
        if (capsuleCollider == null)
        {
            return;
        }

        isCrouching = true;
        capsuleCollider.height = crouchHeight;

    }
    public void StopCrouch()
    {
        if (Physics.Raycast(transform.position, Vector3.up, 1f))
        {
            return;
        }

        isCrouching = false;
        if (capsuleCollider != null)
        {
            capsuleCollider.height = standingHeight;
        }
    }

    private void OnDestroy()
    {
        if (playerInputActions != null)
        {
            playerInputActions.Player.Sprint.performed -= OnSprintPerformed;
            playerInputActions.Player.Sprint.canceled -= OnSprintCanceled;
            playerInputActions.Player.Crouch.performed -= OnCrouchPerformed;
            playerInputActions.Player.Crouch.canceled -= OnCrouchCanceled;
            playerInputActions.Player.Disable();
            playerInputActions.Dispose();
            playerInputActions = null;
        }
    }

}
