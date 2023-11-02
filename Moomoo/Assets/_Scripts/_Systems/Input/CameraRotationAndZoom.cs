using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CameraRotationAndZoom : MonoBehaviour
{
    [SerializeField] private CinemachineInputProvider cmInputProvider;
    public void RotationAndZoomInput(bool value)
    {
        cmInputProvider.enabled = value;
    }
}
