using UnityEngine;

[RequireComponent(typeof(Camera))]
public class ResponsiveCamera : MonoBehaviour
{
    // Base orthographic size designed for standard 16:9 aspect ratio
    public float baseOrthographicSize = 15f;
    private float baseAspect = 16.0f / 9.0f;

    private Camera cam;

    void Awake()
    {
        cam = GetComponent<Camera>();
        AdjustCamera();
    }

    void LateUpdate()
    {
        // Continuously adjust for device rotation or screen changes
        AdjustCamera();
    }

    void AdjustCamera()
    {
        float currentAspect = (float)Screen.width / Screen.height;

        if (currentAspect < baseAspect)
        {
            // If the screen is narrower (like tablets 4:3), 
            // increase orthographic size to prevent horizontal cropping.
            cam.orthographicSize = baseOrthographicSize * (baseAspect / currentAspect);
        }
        else
        {
            // For wider screens (phones), keep the base vertical size.
            cam.orthographicSize = baseOrthographicSize;
        }
    }
}