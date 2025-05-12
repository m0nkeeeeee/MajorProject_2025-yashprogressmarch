using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class OutLine : MonoBehaviour
{
    OutLine outline;
    public string message;

    public UnityEvent onInteraction;

    void Start()
    {
        outline = GetComponent<OutLine>();
        DissableOutline();

    }
    public void DissableOutline()
    {
        outline.enabled = false;
    }
    public void EnableOutline()
    {
        outline.enabled = true;
    }

}
