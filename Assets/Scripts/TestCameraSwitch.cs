using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class TestCameraSwitch : MonoBehaviour
{
    public CinemachineVirtualCamera camera1;
    public CinemachineVirtualCamera camera2;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) {
            CameraManager.SwitchCamera(camera1);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            CameraManager.SwitchCamera(camera2);
        }
    }
}
