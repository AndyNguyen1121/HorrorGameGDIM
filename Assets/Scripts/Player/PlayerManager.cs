using UnityEngine;
using UnityEngine.Serialization;

namespace Player
{
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField] private PlayerInputManager inputManager;
        [SerializeField] private PlayerMovementManager movementManager;
        [SerializeField] private CharacterController characterController;
        [FormerlySerializedAs("cameraTarget")] [SerializeField] private Transform characterRotator;

        public PlayerInputManager InputManager => inputManager;
        public PlayerMovementManager MovementManager => movementManager;
        public CharacterController CharacterController => characterController;
        public Transform CharacterRotator => characterRotator;
        
        public Camera MainCamera => Camera.main;

    }
}