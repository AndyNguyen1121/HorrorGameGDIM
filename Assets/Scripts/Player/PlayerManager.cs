using System;
using DG.Tweening;
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
        private Transform originalParent;

        public PlayerInputManager InputManager => inputManager;
        public PlayerMovementManager MovementManager => movementManager;
        public CharacterController CharacterController => characterController;
        public Transform CharacterRotator => characterRotator;
        
        public Camera MainCamera => Camera.main;

        [Header("Flags")] 
        public bool isGrabbed;

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

        public void GrabPlayer(Transform grabPoint, float duration)
        {
            characterController.enabled = false;
            isGrabbed = true;
            transform.parent = grabPoint;
            transform.DOLocalMove(Vector3.zero, duration);
            transform.DORotateQuaternion(grabPoint.rotation, duration);
        }

        public void UngrabPlayer()
        {
            characterController.enabled = true;
            isGrabbed = false;
            transform.parent = originalParent;
        }

    }
}