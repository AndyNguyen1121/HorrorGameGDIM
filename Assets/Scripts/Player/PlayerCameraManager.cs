using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design;
using DG.Tweening;
using Unity.Cinemachine;
using UnityEngine;

namespace Player
{
    public class PlayerCameraManager : MonoBehaviour
    {
        [SerializeField] private PlayerManager playerManager;
        [SerializeField] private CinemachineCamera IdleCamera;
        [SerializeField] private CinemachineCamera WalkingCamera;
        [SerializeField] private CinemachineCamera RunningCamera;
        [SerializeField] private CinemachineMixingCamera mixingCamera;
        [SerializeField] private float blendDuration = 0.5f;

        private List<Tween> Tweens = new();
        public CinemachineCamera currentCamera;

        private void Start()
        {
            SwitchCameraNoiseProfile(IdleCamera);
        }

        private void Update()
        {
            HandleProfileSwitching();
        }

        private void HandleProfileSwitching()
        {
            if (playerManager.InputManager.IsSprinting)
            {
                SwitchCameraNoiseProfile(RunningCamera);
            }
            else if (playerManager.InputManager.MovementInput != Vector2.zero)
            {
                SwitchCameraNoiseProfile(WalkingCamera);
            }
            else
            {
                SwitchCameraNoiseProfile(IdleCamera);
            }
        }

        private void SwitchCameraNoiseProfile(CinemachineCamera CameraToSwitch)
        {
            if (currentCamera == CameraToSwitch)
                return;
            
            if (Tweens.Count > 0)
            {
                foreach (Tween tween in Tweens)
                {
                    tween.Kill();
                }

                Tweens.Clear();
            }

            foreach (var Camera in mixingCamera.ChildCameras)
            {
                if (Camera == CameraToSwitch)
                {
                    Tween weightTween = DOTween.To(() =>  mixingCamera.GetWeight(Camera), x => mixingCamera.SetWeight(Camera, x), 1, blendDuration);
                    Tweens.Add(weightTween);
                }
                else
                {
                    Tween weightTween = DOTween.To(() =>  mixingCamera.GetWeight(Camera), x => mixingCamera.SetWeight(Camera, x), 0, blendDuration);
                    Tweens.Add(weightTween);
                }
            }

            currentCamera = CameraToSwitch;
        }
    }
}