using UnityEngine;

public class Box : MonoBehaviour
{
    public Interactable OpenFromInteraction;

    private void OnEnable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted += LiftBox;
        }
    }
    private void OnDisable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted -= LiftBox;
        }
    }
    public void LiftBox()
    {
        Debug.Log("this shit works");
        Vector3 pos = transform.position;
        transform.position = new Vector3(pos.x, pos.y + 2, pos.z);
    }
}
