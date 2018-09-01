using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private const float Y_ANGLE_MIN = 0.0f;
    private const float Y_ANGLE_MAX = 50.0f;

    public Transform lookAt;
    public Transform camTransform;
    public float distance = 6.0f;

    private float currentX = 0.0f;
    private float currentY = 45.0f;
    private float sensitivityX = 4.0f;
    private float sensitivityY = 1.0f;
    private float offsetX = 13;
    private float offsetY = 15;

    private void Start()
    {
    }

    private void Update()
    {
        currentX += Input.GetAxis("HorizontalTurn") * sensitivityX;
        currentY += Input.GetAxis("VerticalTurn") * sensitivityY;

        currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
    }

    private void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        camTransform.position = lookAt.position + rotation * dir;
        Vector3 targetView = lookAt.position;
        camTransform.LookAt(targetView);
        //maintain player in bottom left corner
        camTransform.Rotate(offsetX * Vector3.left, Space.Self);
        camTransform.Rotate(offsetY * Vector3.up, Space.World);

        //Quaternion xQuaternion = Quaternion.AngleAxis(offsetX, Vector3.up);
        //Quaternion yQuaternion = Quaternion.AngleAxis(offsetY, Vector3.left);
        //camTransform.localRotation = camTransform.localRotation * xQuaternion * yQuaternion;
        //camTransform.Rotate(offsetX, offsetY, 0);
    }
}
