using UnityEngine;

public class Coin : MonoBehaviour
{
    public GameObject coin;
    public Rigidbody rb;
    void Start()
    {
        CoinToss();
    }
    public Interactable OpenFromInteraction;

    public void CoinToss()
    {
        int JumpForce = Random.Range(400, 0);
        rb.AddForce(0, JumpForce, 0);
        int Spinx = Random.Range(200, 0);
        int Spinz = Random.Range(200, 0);
        rb.AddTorque(Spinx, 0, Spinz);
    }
}
