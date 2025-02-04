using System.Collections;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Camera fpsCamera;

    [SerializeField] float moveSpeed = 9;
    [SerializeField] float runMultiplier = 1.5f;
    [SerializeField] float jumpForce = 5;
    [SerializeField] float gravity = 10;

    public float mouseSensitivityX, mouseSensitivityY;
    [SerializeField] float lookXLimit = 60;
    float rotationX = 0; //stores the x rotation of the camera (vertical in-game)
    Vector3 moveDirection;

    [SerializeField] float maxStamina = 100;
    [SerializeField] float currentStamina = 100;
    [SerializeField] float staminaDrain = 10;
    [SerializeField] float staminaRegen = 10;
    [SerializeField] bool isSprinting = false;
    [SerializeField] bool canSprint = true;
    
    CharacterController controller;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        controller = GetComponent<CharacterController>();
        fpsCamera = Camera.main;
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        #region rotation
        //Horizontal Camera Rotation
        transform.Rotate(Vector3.up * mouseSensitivityX * Time.deltaTime * Input.GetAxis("Mouse X"));
        //Vertical Camera Rotation
        rotationX += Input.GetAxis("Mouse Y") * mouseSensitivityY * Time.deltaTime;
        rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);
        fpsCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
        #endregion

        #region movement
        if (controller.isGrounded)
        {
            float horizontalInput = Input.GetAxis("Horizontal");
            float verticalInput = Input.GetAxis("Vertical");
            float movementDirectionY = moveDirection.y;
            moveDirection = (horizontalInput * transform.right) + (verticalInput * transform.forward);

            if (Input.GetButtonDown("Jump"))
            {
                moveDirection.y = jumpForce;
            }
            else
            {
                moveDirection.y = movementDirectionY;
            }

            if (Input.GetButtonDown("Sprint") && currentStamina > 0 && canSprint)
            {
                isSprinting = true;
                moveSpeed *= runMultiplier;
            }
            if (Input.GetButtonUp("Sprint") && moveSpeed > 9)
            {
                isSprinting = false;
                moveSpeed /= runMultiplier;
            }
            if (isSprinting && currentStamina > 0)
            {
                currentStamina -= staminaDrain * Time.deltaTime;
            }
            if (currentStamina <= 0)
            {
                currentStamina = 0;
                isSprinting = false;
                moveSpeed = 9;
                StartCoroutine(StaminaRegenTimer());
            }
            if (currentStamina > maxStamina)
            {
                currentStamina = maxStamina;
            }
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }
        controller.Move(moveDirection * moveSpeed * Time.deltaTime);
        #endregion
    }

    IEnumerator StaminaRegenTimer()
    {
        isSprinting = false;
        canSprint = false;
        yield return new WaitForSeconds(2);
        currentStamina += staminaRegen * Time.deltaTime;
        yield return new WaitForSeconds(1);
        canSprint = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Ammo")
        {
            if (GameObject.FindFirstObjectByType<PlayerShooting>().ammo != 6)
            {
                Destroy(other.gameObject);
                GameObject.FindFirstObjectByType<PlayerShooting>().ammo += 3;
            }
        }
    }
}
