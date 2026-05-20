using UnityEngine;

public class Player : MonoBehaviour
{
    
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private Transform cameraTransform;
    
   private void Update()
    {
        Vector2 inputVector = playerMovement.GetMovementVectorNormalized();

        Vector3 forward = cameraTransform.forward;
        Vector3 right = cameraTransform.right;

        forward.y = 0f;
        right.y = 0f;

        forward.Normalize();
        right.Normalize();

        float currentSpeed = playerMovement.GetCurrentSpeed();

        Vector3 moveDir = forward * inputVector.y + right * inputVector.x;
        transform.position += moveDir * currentSpeed * Time.deltaTime;
       
    }
}
