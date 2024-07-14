using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FPS_Move : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    public float jumpForce = 7.5f;
    public float mouseSensitivity = 100.0f;
    public Transform playerCamera;

    private Rigidbody rb;
    private float xRotation = 0f;

    private float sprintSpeed;

    public GameObject weapon;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Cursor.lockState = CursorLockMode.Locked;

        sprintSpeed = moveSpeed * 1.5f;
    }

    void Update()
    {
        LookAround();
        GameMode();
    }

    void FixedUpdate()
    {
        MovePlayer();
    }

    void LookAround()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        playerCamera.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        transform.Rotate(Vector3.up * mouseX);
    }

    void MovePlayer()
    {
        float moveX = Input.GetAxis("Horizontal");
        float moveZ = Input.GetAxis("Vertical");

        Vector3 movement = transform.right * moveX + transform.forward * moveZ;
        if (Input.GetKey(KeyCode.Space)) //&& rb.velocity.y == 0
        {
            //rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            rb.velocity = Vector3.up * jumpForce;
        }
        else if (Input.GetKey(KeyCode.LeftControl))
        {
            rb.velocity = Vector3.down * jumpForce;
        }
        else if (Input.GetKey(KeyCode.LeftShift))
        {
            rb.velocity = movement * sprintSpeed + new Vector3(0, rb.velocity.y, 0);
        }
        else
        {
            rb.velocity = movement * moveSpeed + new Vector3(0, 0, 0); //rb.velocity.y
        }
    }
    void GameMode()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            weapon.SetActive(!weapon.activeSelf);
        }
    }
}