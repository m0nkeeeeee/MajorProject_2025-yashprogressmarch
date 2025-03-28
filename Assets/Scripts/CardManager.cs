using System.Collections.Generic;
using UnityEngine;

public class CardManager : MonoBehaviour
{
    [HideInInspector] public int pairAmount = 10; // 10 pairs (tileimage_1 to tileset2_10)
    public GameObject cardPrefab;

    public List<Sprite> tileImages = new List<Sprite>(); // tileimage_1 to 10
    public List<Sprite> tileSet2Images = new List<Sprite>(); // tileset2_1 to 10

    private List<GameObject> cardDeck = new List<GameObject>();
    [HideInInspector]public int width;
    [HideInInspector]public int height;
    float offSet = 1.2f;

    void Start()
    {
        GameManager.instance.SetPairs(pairAmount);
        CreatePlayField();
    }

    void CreatePlayField()
    {
        // Pair tileimage_X with tileset2_X
        for (int i = 0; i < pairAmount; i++)
        {
            // Create the first card (tileimage_X)
            GameObject cardA = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            cardA.GetComponent<Card>().SetCard(i, tileImages[i]);
            cardDeck.Add(cardA);

            // Create the matching pair card (tileset2_X)
            GameObject cardB = Instantiate(cardPrefab, Vector3.zero, Quaternion.identity);
            cardB.GetComponent<Card>().SetCard(i, tileSet2Images[i]);
            cardDeck.Add(cardB);
        }

        // Shuffle the cards
        for (int i = 0; i < cardDeck.Count; i++)
        {
            int index = Random.Range(0, cardDeck.Count);
            (cardDeck[i], cardDeck[index]) = (cardDeck[index], cardDeck[i]);
        }

        // Place cards in grid
        int num = 0;
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                if (num >= cardDeck.Count) break;
                cardDeck[num].transform.position = new Vector3(x * offSet, 0, z * offSet);
                num++;
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                Vector3 pos = new Vector3(x * offSet, 0, z * offSet);
                Gizmos.DrawWireCube(pos, new Vector3(1,0.1f,1));
            }
        }

    }
}
