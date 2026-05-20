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


    private void Awake()
    {
        chRigidbody = GetComponent<Rigidbody>();
        if (groundCheck == null)
        {
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        //Sprinting
        playerInputActions.Player.Sprint.performed += ctx => isSprinting = true;
        playerInputActions.Player.Sprint.canceled += ctx => isSprinting = false;
        
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
    {   //if isSprinting is true  returns sprintSpeed, if false return walkSpeed. 
        return isSprinting ? sprintSpeed : walkSpeed;
    }


}
