using UnityEngine;

public class Lifetime : MonoBehaviour
{
    public float lifetime = 0f;
    void Start()
    {
        Destroy(gameObject, lifetime);
    }
}
