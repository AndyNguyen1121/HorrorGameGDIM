using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Player
{
    public class PlayerInputManager : MonoBehaviour
    {
        [Header("Assign in Inspector")] [SerializeField]
        private PlayerInput PlayerInput;

        [Header("Input Info")]
        public float sensitivity;
        [SerializeField] private Vector2 movementInput;
        [SerializeField] private Vector2 mouseInput;
        [SerializeField] private bool isSprinting;
        public Vector2 MovementInput => movementInput;
        public Vector2 MouseInput => mouseInput;
        public bool IsSprinting => isSprinting;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }


        private void OnEnable()
        {
            PlayerInput.actions["Move"].performed += OnMove;
            PlayerInput.actions["Move"].canceled += OnMoveCanceled;
            PlayerInput.actions["Sprint"].performed += ctx => isSprinting = true;
            PlayerInput.actions["Sprint"].canceled += ctx => isSprinting = false;
        }

        private void OnDisable()
        {
            PlayerInput.actions["Move"].performed -= OnMove;
            PlayerInput.actions["Move"].canceled -= OnMoveCanceled;
        }

        private void OnMove(InputAction.CallbackContext ctx)
        {
            movementInput = ctx.ReadValue<Vector2>();
        }
        private void OnMoveCanceled(InputAction.CallbackContext ctx)
        {
            movementInput = Vector2.zero;;
        }
       
        
    }
}