using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class Camera360 : MonoBehaviour {

    public RenderTexture leftEye;
    public RenderTexture rightEye;
    public RenderTexture equirect;
    public bool renderStereo = true;
    public float stereoSeparation = 0.064f;

    void LateUpdate()
    {
        Camera cam = GetComponent<Camera>();

        if (cam == null)
        {
            cam = GetComponentInParent<Camera>();
        }

        if (cam == null)
        {
            Debug.Log("stereo 360 capture node has no camera or parent camera");
        }

        if (renderStereo)
        {
            cam.stereoSeparation = stereoSeparation;
            cam.RenderToCubemap(leftEye, 63, Camera.MonoOrStereoscopicEye.Left);
            cam.RenderToCubemap(rightEye, 63, Camera.MonoOrStereoscopicEye.Right);
        }
        else
        {
            cam.RenderToCubemap(leftEye, 63, Camera.MonoOrStereoscopicEye.Mono);
        }

        //optional: convert cubemaps to equirect
        leftEye.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Left);

        rightEye.ConvertToEquirect(equirect, Camera.MonoOrStereoscopicEye.Right);

        if (equirect == null)
            return;
    }
}

