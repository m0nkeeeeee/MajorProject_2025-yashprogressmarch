using System.Collections;
using UnityEngine;
using TMPro;
using UnityEngine.UI;   

public class CardScoreManager : MonoBehaviour
{

    public static CardScoreManager instance;
    public int timeForLevelToComplete = 120;
    public Image timeImage;
    public TMP_Text timeText;
    public TMP_Text scoreText;
    public TMP_Text turnsText;


    int score;
    int turns;

    void Awake()
    {
        instance = this;
    }
    void Start()
    {
        StartCoroutine("Timer");
        AddScore(0);
    }

    IEnumerator Timer()
    {
        int tempTime = timeForLevelToComplete;
        timeText.text = timeForLevelToComplete.ToString();
        while (tempTime > 0)
        {
            tempTime--;
            yield return new WaitForSeconds(1);

            timeImage.fillAmount = tempTime / (float)timeForLevelToComplete;
            timeText.text = tempTime.ToString();

        }
    // GAME OVER 
    GameManager.instance.GameOver();

    }


    public void AddScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = score.ToString("D8");
    }


    public void AddTurn()
    {
        turns++;
        turnsText.text = turns.ToString("D2");
    }

}
