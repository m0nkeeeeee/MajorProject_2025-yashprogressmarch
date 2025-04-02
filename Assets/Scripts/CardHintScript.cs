using UnityEngine;
using UnityEngine.UI;

public class HintDisplay : MonoBehaviour
{
    public GameObject hintImage; // Assign the Image GameObject in Inspector

    void Update()
    {
        if (Input.GetKey(KeyCode.H))
        {
            hintImage.SetActive(true);
        }
        else
        {
            hintImage.SetActive(false);
        }
    }
}
