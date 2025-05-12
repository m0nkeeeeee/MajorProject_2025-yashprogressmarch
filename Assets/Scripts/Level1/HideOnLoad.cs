using JetBrains.Annotations;
using UnityEngine;

public class HideOnLoad : MonoBehaviour
{
    public GameObject canvas;
    void Start()
    {
        canvas.SetActive(false);
    }


}
