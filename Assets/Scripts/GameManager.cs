using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;
using UnityEngine.UI; // Import UI namespace

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool picked;
    bool gameOver;
    int pairs;
    int pairCounter;
    public bool hideMatches;
    public int scorePerMatch = 100;

    List<Card> pickedCards = new List<Card>();

    [Header("Hint System")] // Group hint-related properties in the Inspector
    public GameObject hintImage; // Assign UI Image in Inspector

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        if (hintImage != null)
        {
            hintImage.SetActive(false); // Ensure the hint is hidden at start
        }
    }

    private void Update()
    {
        if (hintImage != null)
        {
            if (Input.GetKey(KeyCode.H))
            {
                hintImage.SetActive(true);
            }
            else
            {
                hintImage.SetActive(false);
            }
        }
    }

    public void AddCardPickedList(Card card)
    {
        pickedCards.Add(card);
        if (pickedCards.Count == 2)
        {
            picked = true;
            StartCoroutine(CheckMatch());
        }
    }

    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(0.5f);
        if (pickedCards[0].GetCardId() == pickedCards[1].GetCardId())
        {
            if (hideMatches)
            {
                pickedCards[0].gameObject.SetActive(false);
                pickedCards[1].gameObject.SetActive(false);
            }
            else
            {
                pickedCards[0].GetComponent<BoxCollider>().enabled = false;
                pickedCards[1].GetComponent<BoxCollider>().enabled = false;
            }

            pairCounter++;
            CheckForWin();
            CardScoreManager.instance.AddScore(scorePerMatch);
        }
        else
        {
            pickedCards[0].FlipOpen(false);
            pickedCards[1].FlipOpen(false);
        }

        yield return new WaitForSeconds(0.5f);
        picked = false;
        pickedCards.Clear();
        CardScoreManager.instance.AddTurn();
    }

    void CheckForWin()
    {
        if (pairs == pairCounter)
        {
            Debug.Log("You win!");
        }
    }

    public void GameOver()
    {
        gameOver = true;
        Debug.Log("Game Over");
    }

    public bool HasPicked()
    {
        return picked;
    }
    public bool IsGameOver()
    {
        return gameOver;
    }

    public void SetPairs(int pairAmount)
    {
        pairs = pairAmount;
    }
}
