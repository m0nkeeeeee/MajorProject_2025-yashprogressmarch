using UnityEngine;

public class DoorCloseTrigger : MonoBehaviour
{
    public DoorController door;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            door.CloseDoor();
        }
    }
}
