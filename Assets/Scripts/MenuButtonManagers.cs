using UnityEngine;

public class MenuButtonManagers : MonoBehaviour
{
    [SerializeField] MenuManagers.MainMenuButtons _buttionType;
    public void ButtonClicked()
    {
        MenuManagers._.MainMenuButtonsClicked(_buttionType);
    }
}
