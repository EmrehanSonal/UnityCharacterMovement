using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 7f;
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

        Vector3 moveDir = forward * inputVector.y + right * inputVector.x;
        transform.position += moveDir * moveSpeed * Time.deltaTime;
       
    }
}
