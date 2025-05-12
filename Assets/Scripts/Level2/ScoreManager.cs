using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int score = 0;
    public DoorController doorController;
    public TextMeshProUGUI scoreText; // Assign a UI Text element in Inspector

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        UpdateScoreText();
    }
    private bool isOpen = true;
    private void Update()
    {
        if (score >= 200 && isOpen)
        {
            doorController.OpenDoor();
            isOpen = false;
        }
    }

    public void AddScore(int value)
    {
        int previousScore = score; // Store old score
        score = Mathf.Clamp(score + value, 0, 200); // Clamp within range

        UpdateScoreText();

        // Open door if score reaches 200 for the first time
        if (previousScore < 200 && score >= 200)
        {
            doorController.OpenDoor();
        }
    }

    public void RemoveScore(int value)
    {
        score = Mathf.Clamp(score - value, 0, 200); // Ensure score doesn't drop below 0
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = "Score: " + score;
    }
}
