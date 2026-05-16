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
        public Transform originalParent;

        public PlayerInputManager InputManager => inputManager;
        public PlayerMovementManager MovementManager => movementManager;
        public CharacterController CharacterController => characterController;
        public Transform CharacterRotator => characterRotator;
        
        public Camera MainCamera => Camera.main;
        

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

        public void EnablePlayerMovement(bool Active)
        {
            characterController.enabled = Active;
        }

    }
}