using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    InteractEvent interact = new InteractEvent();
    Player Player;
    public InteractEvent GetInteractEvent
    {
        get
        {
            if (interact == null) interact = new InteractEvent();
            return interact;
        }

    }
    public Player GetPlayer
    {
        get
        {
            return Player;
        }
    }
    public void CallInteractEvent(Player interactedPlayer)
    {
        Player = interactedPlayer;
        interact.CallInteractEvent();
    }


}

public class InteractEvent
{
    public delegate void InteractHandler();
    public event InteractHandler HasInteracted;
    public void CallInteractEvent() => HasInteracted?.Invoke();
}