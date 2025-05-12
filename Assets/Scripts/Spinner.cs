using UnityEngine;

public class Spinner : MonoBehaviour
{
    [SerializeField] private float spinSpeed = 100f; // Speed in degrees per second

    void Update()
    {
        // Rotate the mesh locally without affecting the Rigidbody parent
        transform.Rotate(Vector3.up * spinSpeed * Time.deltaTime, Space.Self);
    }
}
