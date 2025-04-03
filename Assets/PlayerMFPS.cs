using UnityEngine;

public class PlayerMFPS : MonoBehaviour
{
    [SerializeField] float mouseSens = 3f;
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] float jumpSpeed = 5f;
    [SerializeField] float mass = 1f;
    [SerializeField] Transform cameraTransform;
    CharacterController controller;

    Vector3 velocity;
    Vector2 look;


    void Awake()
    {
        controller = GetComponent<CharacterController>();
      
    }
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }
    public void Teleport(Vector3 position, Quaternion rotation)
    {
        transform.position = position;
        Physics.SyncTransforms();
        look.x = rotation.eulerAngles.y;
        look.y = rotation.eulerAngles.x;
        velocity = Vector3.zero;
    }

    void Update()
    {
        UpdateGravity();
        UpdateLook();
        UpdateMovement();
    }

    void UpdateGravity()
    {
        var gravity = Physics.gravity * mass * Time.deltaTime;
        velocity.y += controller.isGrounded ? -1f : velocity.y + gravity.y;
    }

    void UpdateMovement()
    {
        var x = Input.GetAxis("Horizontal");
        var y = Input.GetAxis("Vertical");

        var input = new Vector3();
        input += transform.forward * y;
        input += transform.right * x;
        input = Vector3.ClampMagnitude(input, 1f);

        if(Input.GetButtonDown("Jump") && controller.isGrounded)
        {
            velocity.y += jumpSpeed;
        }

        controller.Move((input * moveSpeed + velocity)* Time.deltaTime);

    }

    void UpdateLook()
    {
        look.x = Input.GetAxis("Mouse X");
        look.y = Input.GetAxis("Mouse Y");

        look.y = Mathf.Clamp(look.y, -89f, 89f);

        cameraTransform.localRotation *= Quaternion.Euler(-look.y, 0f, 0f);
        transform.localRotation *= Quaternion.Euler(0f, look.x, 0f);

    }
}
