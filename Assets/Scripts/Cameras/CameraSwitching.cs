using System;
using Player;
using Unity.Cinemachine;
using UnityEngine;

namespace Cameras
{
    [RequireComponent(typeof(BoxCollider))]
    public class CameraSwitching : MonoBehaviour
    {
        [SerializeField] private UnityEngine.Camera CameraToSwitch;

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Player"))
            {
                if (PlayerManager.Instance.currentCamera != CameraToSwitch)
                {
                    PlayerManager.Instance.currentCamera = CameraToSwitch;
                    CameraManager.instance.ActivateCamera(CameraToSwitch);
                    PlayerManager.Instance.MovementManager.DisableChangingDirection(0.25f);
                }
            }
        }
    }
}