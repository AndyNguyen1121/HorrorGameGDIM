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
        [FormerlySerializedAs("cameraTarget")] [SerializeField] private Transform characterRotator;
        public Transform originalParent;

        public PlayerInputManager InputManager => inputManager;
        public PlayerMovementManager MovementManager => movementManager;
        public CharacterController CharacterController => characterController;
        public Transform CharacterRotator => characterRotator;
        
        public UnityEngine.Camera MainCamera => UnityEngine.Camera.main;

        public UnityEngine.Camera currentCamera;

        public event Action OnSpacePressed;

        public bool canMove = true;
        

        private void Awake()
        {
            if (Instance == null)
            {
                Instance = this;
            }
            else
            {
                Destroy(gameObject);
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
        }

        public void EnablePlayerMovement(bool Active)
        {
            canMove = Active;
        }

    }
}