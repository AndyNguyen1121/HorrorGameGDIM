using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Cameras
{
    public class CameraManager : MonoBehaviour
    {
        [SerializeField] List<UnityEngine.Camera> Cameras = new List<UnityEngine.Camera>();
        
        public static CameraManager instance;

        private void Awake()
        {
            if (instance == null)
                instance = this;
            else
                Destroy(gameObject);
        }

        private void Start()
        {
            Cameras = FindObjectsByType<UnityEngine.Camera>(FindObjectsInactive.Include, FindObjectsSortMode.None).ToList();
        }

        public void ActivateCamera(UnityEngine.Camera cam)
        {
            foreach (var camera in Cameras)
            {
                camera.enabled = false;
            }
            
            cam.enabled = true;
        }
    }
}