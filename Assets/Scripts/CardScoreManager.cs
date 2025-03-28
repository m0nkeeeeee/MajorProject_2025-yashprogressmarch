using System.Collections;
using UnityEngine;

public class CardScoreManager : MonoBehaviour
{
    public int timeForLevelToComplete = 120;

    void Start()
    {
        StartCoroutine("Timer");
    }

    IEnumerator Timer()
    {
        int tempTime = timeForLevelToComplete;
        while(tempTime > 0)
        {
            tempTime--;
            yield return new WaitForSeconds(1);

        }
    // GAME OVER 
    GameManager.instance.GameOver();

    }
}
