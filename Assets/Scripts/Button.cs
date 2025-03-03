using UnityEngine;

public class Buttion : MonoBehaviour
{

    public Interactable OpenFromInteraction;
    public Coin coin;

    void Start()
    {

    }

    private void OnEnable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted += CallCoin;
        }
    }
    private void OnDisable()
    {
        if (OpenFromInteraction)
        {
            OpenFromInteraction.GetInteractEvent.HasInteracted -= CallCoin;
        }
    }
    public void CallCoin()
    {
        coin.CoinToss();
    }
}
