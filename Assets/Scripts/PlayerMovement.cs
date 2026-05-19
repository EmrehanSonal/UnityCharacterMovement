using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private Rigidbody chRigidbody;

    private void Awake()
    {
        chRigidbody = GetComponent<Rigidbody>();
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
        Debug.Log("Jump");
        chRigidbody.AddForce(Vector3.up * 2f, ForceMode.Impulse);
    }

}
