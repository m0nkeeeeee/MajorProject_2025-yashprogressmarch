using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PlayerInteract();
    }
    public void PlayerInteract()
    {
        var layermask0 = 1 << 0;
        var layermask3 = 1 << 3;
        var finalmask = layermask0 | layermask3;

        RaycastHit hit;
        Ray ray = Camera.main.ViewportPointToRay(new Vector3(.5F, .5F, 0));

        if (Physics.Raycast(ray, out hit, 15, finalmask))
        {
            Interactable interactscript = hit.transform.GetComponent<Interactable>();
            if (interactscript) interactscript.CallInteractEvent(this);
        }
    }
}
