using UnityEngine;

public class DoorController : MonoBehaviour
{
    public float moveSpeed = 3f;
    public float moveDistance = 10f; // Moves 10 units underground
    private Vector3 closedPosition;
    private Vector3 openPosition;
    private bool isMoving = false;
    private Vector3 targetPosition;

    void Start()
    {
        closedPosition = transform.position; // Door starts at ground level
        openPosition = closedPosition - new Vector3(0, moveDistance, 0); // Moves down 10 units
        targetPosition = closedPosition; // Default state is closed
    }

    public void OpenDoor()
    {
        Debug.Log("Opening Door...");
        targetPosition = openPosition;
        isMoving = true;
    }

    public void CloseDoor()
    {
        Debug.Log("Closing Door...");
        targetPosition = closedPosition;
        isMoving = true;
    }

    void Update()
    {
        if (isMoving)
        {
            transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector3.Distance(transform.position, targetPosition) < 0.01f)
            {
                Debug.Log("Door reached target position.");
                isMoving = false; // Stops movement when target is reached
            }
        }
    }
}
