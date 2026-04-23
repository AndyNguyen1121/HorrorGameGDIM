using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerMovementManager : MonoBehaviour
    {
        private PlayerManager playerManager;
        
        [Header("Movement Attributes")] 
        [SerializeField] private float walkSpeed = 5f;
        [SerializeField] private float sprintSpeed = 12f;
        [SerializeField] private float rotationSpeed;
        [SerializeField] private float gravity = -9.81f;
        

        private float verticalVelocity;

        [Header("Ground Check")] 
        [SerializeField] private float groundCheckRadius;
        [SerializeField] private Vector3 groundCheckOffset;
        [SerializeField] private LayerMask groundLayer;

        private Vector3 directionRelativetoCamera;
        
        private void Awake()
        {
            playerManager = GetComponent<PlayerManager>();
        }

        // Update is called once per frame
        private void Update()
        {
            HandleMovement();
            HandleRotation();
        }

        private void HandleMovement()
        {
            Vector2 cachedInputDirection = playerManager.InputManager.MovementInput;
            Vector3 moveVelocity = Vector3.zero;
            Transform cachedCameraTransform = playerManager.MainCamera.transform;
            
            Vector3 flatCameraForward = cachedCameraTransform.forward;
            flatCameraForward.y = 0f;
            flatCameraForward.Normalize();
            
            Vector3 flatCameraRight = cachedCameraTransform.right;
            flatCameraRight.y = 0f;
            flatCameraRight.Normalize();
            
            
            if (cachedInputDirection != Vector2.zero)
            {
                // XZ movement
                moveVelocity = flatCameraForward * cachedInputDirection.y;
                moveVelocity += flatCameraRight * cachedInputDirection.x;
                directionRelativetoCamera = moveVelocity.normalized;
            }

            if (!IsGrounded())
            {
                // adjust for gravity
                verticalVelocity += gravity * Time.deltaTime;
            }
            else
            {
                verticalVelocity = 0;
            }
            
            // apply final XZ movement and gravity
            if (playerManager.InputManager.IsSprinting)
            {
                moveVelocity *= sprintSpeed;
            }
            else
            {
                moveVelocity *= walkSpeed;
            }
            moveVelocity += Vector3.up * verticalVelocity;
            playerManager.CharacterController.Move(moveVelocity * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (playerManager.InputManager.MovementInput == Vector2.zero)
                return;

            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(directionRelativetoCamera), rotationSpeed * Time.deltaTime);
        }

        public bool IsGrounded()
        {
            return Physics.OverlapSphere(transform.position + groundCheckOffset, groundCheckRadius, groundLayer).Length > 0;
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            Gizmos.DrawWireSphere(transform.position + groundCheckOffset, groundCheckRadius);
        }
    }
}