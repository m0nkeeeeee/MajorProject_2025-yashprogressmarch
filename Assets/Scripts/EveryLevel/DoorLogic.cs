using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class DoorLogic : MonoBehaviour
{
    public Interactable OpenFromInteraction;
    public string LevelToEnter;

    private void OnEnable()
    {
        OpenFromInteraction.GetInteractEvent.HasInteracted += CallFunc;
    }
    private void OnDiassable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted -= CallFunc;
        }
    }
    private void CallFunc()
    {
        SceneManager.LoadScene(LevelToEnter);
    }
}
