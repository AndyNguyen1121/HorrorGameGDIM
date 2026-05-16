using UnityEngine;

[ExecuteAlways]
public class CameraBackground : MonoBehaviour
{
    public Camera targetCamera;
    public float distance = 300f;

    void Update()
    {
        if (!targetCamera)
            targetCamera = Camera.main;

        transform.position = targetCamera.transform.position + targetCamera.transform.forward * distance;

        transform.rotation = targetCamera.transform.rotation;

        float height = 2f * distance * Mathf.Tan(targetCamera.fieldOfView * 0.5f * Mathf.Deg2Rad);

        float width = height * targetCamera.aspect;

        transform.localScale = new Vector3(width, height, 1f);
    }
}
