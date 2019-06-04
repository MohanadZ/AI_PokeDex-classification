using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraRotation : MonoBehaviour
{
    [SerializeField] float mouseSensitivity = 10;
    [SerializeField] string mouseXInputName, mouseYInputName;
    [SerializeField] Transform playerBody;

    //private Vector3 eulerRotation;
    private float xAxisClamp = 0.0f; //, mouseX, mouseY;

    // Start is called before the first frame update
    void Start()
    {
        LockCursor();
        mouseXInputName = "Mouse X";
        mouseYInputName = "Mouse Y";
    }

    // Update is called once per frame
    void Update()
    {
        RotateCamera();
    }

    private void LockCursor()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void RotateCamera()
    {
        float mouseX = Input.GetAxis(mouseXInputName) * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis(mouseYInputName) * mouseSensitivity * Time.deltaTime;

        xAxisClamp += mouseY;

        if (xAxisClamp > 90.0f)
        {
            xAxisClamp = 90.0f;
            mouseY = 0;
            ClampxAxisRotationToValue(270.0f);
        }
        else if (xAxisClamp < -90.0f)
        {
            xAxisClamp = -90.0f;
            mouseY = 0;
            ClampxAxisRotationToValue(90.0f);
        }

        transform.Rotate(Vector3.left * mouseY);
        playerBody.Rotate(Vector3.up * mouseX);
    }

    private void ClampxAxisRotationToValue(float value)
    {
        Vector3 eulerRotation = transform.eulerAngles;
        eulerRotation.x = value;
        transform.eulerAngles = eulerRotation;
    }
}
