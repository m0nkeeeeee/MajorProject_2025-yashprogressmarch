using UnityEngine;
using TMPro;
using JetBrains.Annotations;

public class HUD_Controller : MonoBehaviour
{
    public static HUD_Controller instance;
    [SerializeField] TextMeshProUGUI interactText;
    private void Awake()
    {
        interactText.text = "";
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Debug.Log("Multiple HUD Scripts");
        }
    }

    public void EnableInteractionText(string text)
    {
        interactText.text = text;
        interactText.gameObject.SetActive(true);
    }
    public void DisableInteractionText()
    {
        interactText.gameObject.SetActive(false);
        interactText.text = string.Empty;
    }

}
