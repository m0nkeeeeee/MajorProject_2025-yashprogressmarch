using System.Collections;
using UnityEngine;

public class ButtionLVL2 : MonoBehaviour
{
    public Interactable OpenFromInteraction;
    public Spawner spawner;
    public DoorController doorController;
    public DialogueLVL2 dialogue;
    public float spawnRate = 1f; // 2 objects per second
    private bool hasBeenPressed = false;

    void OnEnable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted += HandleInteraction;
        }
    }

    void OnDisable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted -= HandleInteraction;
        }
    }

    private void HandleInteraction()
    {
        if (!hasBeenPressed)
        {
            hasBeenPressed = true; // Disable further interactions
            dialogue.StartDialogue();
            StartCoroutine(SpawnObjectsRoutine());
            doorController.OpenDoor();
        }
    }

    private IEnumerator SpawnObjectsRoutine()
    {
        while (ScoreManager.instance.score < 200) // Keep spawning until score reaches 200
        {
            spawner.SpawnSphere();
            yield return new WaitForSeconds(spawnRate); // Spawn every 0.5 seconds
        }
        Debug.Log("Spawning stopped, score reached 200.");
    }
}
