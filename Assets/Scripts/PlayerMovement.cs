
using System.Runtime.CompilerServices;
using JetBrains.Annotations;
using Unity.VisualScripting;
using UnityEngine;

public class Player_Movement : MonoBehaviour
{
    //constants
    public Rigidbody rb;
    public float Walk_Speed = 5;
    public float Run_Speed = 15;
    public Camera cb;
    public Transform head;

    void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
        rb = GetComponent<Rigidbody>();
        cb = GetComponent<Camera>();
    }
    void Update()
    {
        transform.Rotate(Vector3.up * Input.GetAxis("Mouse X") * 2f);
    }
    void FixedUpdate()
    {
        Vector3 Movement = Vector3.up * rb.linearVelocity.y;
        float Speed = Input.GetKey(KeyCode.LeftShift) ? Run_Speed : Walk_Speed;
        Movement.x = Input.GetAxis("Horizontal") * Speed;
        Movement.z = Input.GetAxis("Vertical") * Speed;
        rb.linearVelocity = transform.TransformDirection(Movement);


    }
    void LateUpdate()
    {
        Vector3 mouseX = head.eulerAngles;
        mouseX.x -= Input.GetAxis("Mouse Y") * 2f;
        mouseX.x = RestrictAngle(mouseX.x, -70f, 85f);
        head.eulerAngles = mouseX;
    }

    public static float RestrictAngle(float angle, float Minangle, float Maxangle)
    {
        if (angle > 180)
            angle -= 360;
        else if (angle < -180)
            angle += 360;

        if (angle > Maxangle)
            angle = Maxangle;
        if (angle < Minangle)
            angle = Minangle;
        return angle;
    }
}
