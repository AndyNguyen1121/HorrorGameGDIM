using System;
using Cameras;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        public static PlayerManager Instance;
        [SerializeField] private PlayerInputManager inputManager;
        [SerializeField] private PlayerMovementManager movementManager;
        [SerializeField] private CharacterController characterController;
        [SerializeField] private Transform characterRotator;
        [SerializeField] private Animator animator;
        public Transform originalParent;

        public PlayerInputManager InputManager => inputManager;
        public PlayerMovementManager MovementManager => movementManager;
        public CharacterController CharacterController => characterController;
        public Transform CharacterRotator => characterRotator;
        
        public UnityEngine.Camera MainCamera => UnityEngine.Camera.main;

        public UnityEngine.Camera currentCamera;

        public event Action OnSpacePressed;

        public bool canMove = true;
        
        public event Action OnStruggleStart;
        public event Action OnStruggleEnd;

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            originalParent = transform.parent;
        }

        private void Start()
        {
            CameraManager.instance.ActivateCamera(currentCamera);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Space))
                OnSpacePressed?.Invoke();

            animator.SetBool("IsMoving", canMove && characterController.velocity.magnitude > 0.1f);
        }

        public void EnablePlayerMovement(bool Active)
        {
            canMove = Active;
        }

        public void EnterStruggleState()
        {
            animator.SetBool("IsStruggling", true);
            animator.CrossFadeInFixedTime("Struggle", 0.1f);
            OnStruggleStart?.Invoke();
        }

        public void ExitStruggleState()
        {
            animator.SetBool("IsStruggling", false);
            OnStruggleEnd?.Invoke();
        }
    }
}