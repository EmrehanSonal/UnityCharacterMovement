using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;
    private GroundCheck groundCheck;
    private Rigidbody chRigidbody;

    private void Awake()
    {
        chRigidbody = GetComponent<Rigidbody>();
        if (groundCheck == null)
        {
            groundCheck = GetComponentInChildren<GroundCheck>();
        }

        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
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

}
