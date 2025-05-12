using Unity.Mathematics;
using UnityEngine;

public class SimpleBounceanimation : MonoBehaviour
{
    public float amp = 0.5f;
    public float frequency = 1f;
    public float RaycastLength = 2f;
    Vector3 posOriginal = new Vector3();
    Vector3 posTemp = new Vector3();

    public LayerMask groundLayer;
    private Rigidbody rb;
    private bool isGrounded;
    private bool isBobble;

    void Start()
    {
        posOriginal = transform.position;
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {

        
        isGrounded = Physics.Raycast(transform.position, Vector3.down, RaycastLength, groundLayer);
        
        isBobble = Physics.Raycast(transform.position, Vector3.down, RaycastLength - 2f, groundLayer);
        Debug.DrawRay(transform.position, Vector3.down * RaycastLength, Color.red);

        Debug.Log(isGrounded);
        if (isBobble)
        {
            if (isGrounded)
            {
                posTemp = posOriginal;
                posTemp.y += Mathf.Sin(Time.fixedTime * Mathf.PI * frequency) * amp;
                transform.position = posTemp;
            }
            rb.linearVelocity = Vector3.zero;
        }
        else
        {
            posOriginal = transform.position;
            rb.useGravity = true;
        }
    }
}