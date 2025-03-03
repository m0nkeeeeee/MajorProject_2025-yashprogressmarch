using NUnit.Framework;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.Build.Content;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    bool picked;

    List<Card> pickedCards = new List<Card>();
    private void Awake()
    {
        instance = this;
    }

    public void AddCardPickedList(Card card)
    {
        pickedCards.Add(card);
        if (pickedCards.Count == 2)
        {
            picked = true;
            //check if have a match here
            StartCoroutine(CheckMatch());
        }
    }


    IEnumerator CheckMatch()
    {
        yield return new WaitForSeconds(1);
        if (pickedCards[0].GetCardId() == pickedCards[1].GetCardId())
        {
            //we have a match
            pickedCards[0].gameObject.SetActive(false);
            pickedCards[1].gameObject.SetActive(false);

        }
        else
        {
            pickedCards[0].FlipOpen(false);
            pickedCards[1].FlipOpen(false);

        }

        yield return new WaitForSeconds(1);
        //cleanup
        picked = false;
        pickedCards.Clear();
    }

    public bool HasPicked()
    {
        return picked;
    }
}
