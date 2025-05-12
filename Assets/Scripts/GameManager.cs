using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool picked;
    bool gameOver;
    int pairs;
    int pairCounter;
    public bool hideMatches;
    public int scorePerMatch = 100;
    public CardManager cardManager; // Drag your CardManager here in the Inspector
    public float revealDuration = 4f; // Duration for the cards to stay flipped initially

    List<Card> pickedCards = new List<Card>();

    [Header("Hint System")] // Group hint-related properties in the Inspector
    public GameObject hintImage; // Assign UI Image in Inspector

    public Button flipAllButton;  // Reference to your "Flip All" button
    public float flipDuration = 2f; // Duration for cards to stay flipped when using "Flip All" button

    private void Awake()
    {
        instance = this;
        if (cardManager == null)
            cardManager = FindAnyObjectByType<CardManager>();
    }

    private void Start()
    {
        if (hintImage != null)
        {
            hintImage.SetActive(false); // Ensure the hint is hidden at start
        }
        StartCoroutine(RevealAllCardsAtStart());

        // Make sure the button is linked and adds the method
        if (flipAllButton != null)
        {
            flipAllButton.onClick.AddListener(FlipAllCards);
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

    IEnumerator RevealAllCardsAtStart()
    {
        var deck = cardManager.GetCardDeck(); // Assuming you added a public method to get the card deck

        foreach (var cardObj in deck)
        {
            Card card = cardObj.GetComponent<Card>();
            card.FlipOpen(true);  // Flip all cards open initially
        }

        yield return new WaitForSeconds(revealDuration);

        foreach (var cardObj in deck)
        {
            Card card = cardObj.GetComponent<Card>();
            card.FlipOpen(false);  // Flip all cards back after the reveal duration
        }
    }

    // New method for flipping all cards for a limited time
    public void FlipAllCards()
    {
        var deck = cardManager.GetCardDeck(); // Get the card deck

        // Flip all cards open
        foreach (var cardObj in deck)
        {
            Card card = cardObj.GetComponent<Card>();
            card.FlipOpen(true);  // Flip all cards open
        }

        // Wait for the specified duration (flipDuration)
        StartCoroutine(FlipCardsBackAfterDelay(deck, flipDuration));
    }

    // Coroutine to flip cards back after the delay
    private IEnumerator FlipCardsBackAfterDelay(List<GameObject> deck, float delay)
    {
        yield return new WaitForSeconds(delay);

        // Flip all cards back after the specified duration
        foreach (var cardObj in deck)
        {
            Card card = cardObj.GetComponent<Card>();
            card.FlipOpen(false);  // Flip the cards back
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
