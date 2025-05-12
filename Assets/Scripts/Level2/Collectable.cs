using UnityEngine;

public class Collectable : MonoBehaviour
{
    public ScoreManager scoreManager;
    public int scoreValue = 10; // Points awarded for collecting

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Check if the player touches it
        {
            if (CompareTag("qbit"))
            {
                ScoreManager.instance.AddScore(scoreValue); //Add score
                Destroy(gameObject); // Remove the collectible
            }
            if (CompareTag("bit"))
            {
                ScoreManager.instance.RemoveScore(scoreValue); //Subtract score
                Destroy(gameObject); // Remove the collectible
            }
        }
    }
}