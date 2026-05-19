using UnityEngine;

public class GroundCheck : MonoBehaviour
{
   [SerializeField] private Transform groundCheck;
   [SerializeField] private float groundRadius = 0.2f;
   [SerializeField] private LayerMask groundLayer;

   private bool isGrounded;

    public bool IsGrounded()
    {
        return isGrounded;
    }

    private void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundLayer);
    }
   
}
