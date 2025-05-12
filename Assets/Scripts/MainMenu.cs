using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public void Playgame()
    {
        SceneManager.LoadSceneAsync("1");
    }
}
