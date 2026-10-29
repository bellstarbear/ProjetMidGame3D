using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float mouseSensitivity = 2f;
    public float gravity = -9.81f;

    public Transform playerCamera;

    private CharacterController controller;
    private float verticalVelocity;
    private float cameraRotationX;

    void Start()
    {
        controller = GetComponent<CharacterController>();

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        // รับค่าการเดินจากคีย์บอร์ด
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x
                     + transform.forward * z;

        controller.Move(move * moveSpeed * Time.deltaTime);

        // แรงโน้มถ่วง
        if (controller.isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }

        verticalVelocity += gravity * Time.deltaTime;

        controller.Move(
            Vector3.up * verticalVelocity * Time.deltaTime
        );

        // ควบคุมกล้องด้วยเมาส์
        float mouseX = Input.GetAxis("Mouse X")
                     * mouseSensitivity;

        float mouseY = Input.GetAxis("Mouse Y")
                     * mouseSensitivity;

        transform.Rotate(Vector3.up * mouseX);

        cameraRotationX -= mouseY;
        cameraRotationX = Mathf.Clamp(
            cameraRotationX, -80f, 80f
        );

        if (playerCamera != null)
        {
            playerCamera.localRotation =
                Quaternion.Euler(cameraRotationX, 0f, 0f);
        }

        // กด ESC เพื่อปลดล็อกเมาส์
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        // คลิกเพื่อกลับมาควบคุมกล้อง
        if (Input.GetMouseButtonDown(0))
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}