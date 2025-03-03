using NUnit.Framework;
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
        }
    }

    public bool HasPicked()
    {
        return picked;
    }
}
