using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    [Header("Objects")]
    public Camera playerCamera;

    [Header("Keys")]
    public KeyCode Walk = KeyCode.LeftShift;
    public KeyCode Jump = KeyCode.Space;

    [Header("Preferences")]
    public float rotationSensitivity = 100f;
    public bool invertCamera;

    [Header("General")]
    public float gravityScale = -20f;

    [Header("Movement")]
    public float runSpeed;
    public float walkSpeed;
    public float jumpHeight=1.9f;

    private float cameraVerticalAngle;
    Vector3 moveInput = Vector3.zero;
    Vector3 rotationInput = Vector3.zero;
    CharacterController characterController;
    private void Awake()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        float mouseY = Input.GetAxis("Mouse Y");
        if (invertCamera)
        {
            mouseY = -mouseY;
        }
        Move();
        Look();
    }
    private void Move()
    {
        if (characterController.isGrounded)
        {
            moveInput = new Vector3(Input.GetAxis("Horizontal"), 0f, Input.GetAxis("Vertical"));
            moveInput = Vector3.ClampMagnitude(moveInput, 1f);

            if (Input.GetKey(Walk))
            {
                moveInput = transform.TransformDirection(moveInput) * walkSpeed;
            }
            else
            {
                moveInput = transform.TransformDirection(moveInput) * runSpeed;
            }

            if (Input.GetKeyDown(Jump))
            {
                moveInput.y = Mathf.Sqrt(jumpHeight * -2f * gravityScale);
            }
        }

        moveInput.y += gravityScale * Time.deltaTime;
        characterController.Move(moveInput*Time.deltaTime);
    }

    private void Look()
    {
        rotationInput.x=Input.GetAxis("Mouse X") * rotationSensitivity * Time.deltaTime;
        rotationInput.y = Input.GetAxis("Mouse Y") * rotationSensitivity * Time.deltaTime;

        cameraVerticalAngle = cameraVerticalAngle += rotationInput.y;
        cameraVerticalAngle = Mathf.Clamp(cameraVerticalAngle, -70, 70);

        transform.Rotate(Vector3.up*rotationInput.x);
        playerCamera.transform.localRotation = Quaternion.Euler(+cameraVerticalAngle, 0f, 0f);
    }
}