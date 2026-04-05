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

        private float _pitch;
        private float _yaw;
        [SerializeField]private float minPitch;
        [SerializeField] private float maxPitch;

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
            Vector3 moveDir = Vector3.zero;
            
            if (cachedInputDirection != Vector2.zero)
            {
                // XZ movement
                moveDir = transform.forward * cachedInputDirection.y;
                moveDir += transform.right * cachedInputDirection.x;
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
                moveDir *= sprintSpeed;
            }
            else
            {
                moveDir *= walkSpeed;
            }
            moveDir += Vector3.up * verticalVelocity;
            playerManager.CharacterController.Move(moveDir * Time.deltaTime);
        }

        private void HandleRotation()
        {
            if (playerManager.InputManager.MouseInput == Vector2.zero)
                return;

            Vector2 input = playerManager.InputManager.MouseInput;

            _yaw += input.x * playerManager.InputManager.sensitivity;
            _pitch -= input.y * playerManager.InputManager.sensitivity;
            _pitch = Mathf.Clamp(_pitch, minPitch, maxPitch);

            transform.rotation = Quaternion.Euler(0, _yaw, 0f);
            playerManager.CameraTarget.localRotation = Quaternion.Euler(_pitch, 0f, 0f); 
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