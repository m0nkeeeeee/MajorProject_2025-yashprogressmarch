using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GridSpawner : MonoBehaviour
{
    private Coroutine classicalSearchCoroutine;
    private Coroutine groverSearchCoroutine;

    public GameObject ballPrefab;
    public int gridSize = 10;
    public float spacing = 1.1f;

    private List<GameObject> allBalls = new List<GameObject>();
    private GameObject keyBall;

    public TextMeshProUGUI foundText;

    public void GenerateGrid()
    {
        // Reset UI
        if (foundText != null) foundText.gameObject.SetActive(false);

        foreach (GameObject ball in allBalls)
        {
            Destroy(ball);
        }
        allBalls.Clear();

        for (int x = 0; x < gridSize; x++)
        {
            for (int z = 0; z < gridSize; z++)
            {
                Vector3 position = new Vector3(x * spacing, 0, z * spacing);
                GameObject newBall = Instantiate(ballPrefab, position, Quaternion.identity);
                newBall.name = $"Ball_{x}_{z}";
                allBalls.Add(newBall);
            }
        }

        int keyIndex = Random.Range(0, allBalls.Count);
        keyBall = allBalls[keyIndex];

        // Highlight key early (only for debugging)
        keyBall.GetComponent<Renderer>().material.color = Color.green;

        Debug.Log($"Key Ball is: {keyBall.name}");
    }

    public void StartClassicalSearch()
    {
        if (classicalSearchCoroutine != null)
            StopCoroutine(classicalSearchCoroutine);

        classicalSearchCoroutine = StartCoroutine(ClassicalSearch());
    }

    private IEnumerator ClassicalSearch()
    {
        foreach (GameObject ball in allBalls)
        {
            if (ball == keyBall)
            {
                ball.GetComponent<Renderer>().material.color = Color.green;
                break;
            }
            else
            {
                ball.SetActive(false);
                yield return new WaitForSeconds(0.75f);
            }
        }
    }

    public void StartGroverSearch()
    {
        if (groverSearchCoroutine != null)
            StopCoroutine(groverSearchCoroutine);

        groverSearchCoroutine = StartCoroutine(GroverSearch());
    }

    private IEnumerator GroverSearch()
    {
        int totalSteps = 70;
        float baseDelay = 0.05f;
        float waveSpeed = 0.4f; // Controls how far the wave spreads per step

        // Cache center position
        Vector3 gridCenter = new Vector3((gridSize - 1) * spacing / 2f, 0, (gridSize - 1) * spacing / 2f);

        for (int step = 0; step < totalSteps; step++)
        {
            float currentRadius = step * waveSpeed;

            foreach (GameObject ball in allBalls)
            {
                float distance = Vector3.Distance(ball.transform.position, gridCenter);

                if (distance <= currentRadius)
                {
                    float t = step / (float)(totalSteps - 1); // Lerp factor

                    Renderer renderer = ball.GetComponent<Renderer>();

                    if (ball == keyBall)
                    {
                        Color glowColor = Color.Lerp(Color.green, Color.white, t);
                        renderer.material.color = glowColor;
                        renderer.material.SetColor("_EmissionColor", glowColor * (2.5f + t * 4f));
                        ball.transform.localScale = Vector3.one * (1f + 0.3f * t);
                    }
                    else
                    {
                        Color fadeColor = Color.Lerp(Color.white, Color.black, t);
                        renderer.material.color = fadeColor;
                        renderer.material.SetColor("_EmissionColor", Color.black);
                        ball.transform.localScale = Vector3.one * (1f - 0.4f * t);
                    }
                }
            }

            yield return new WaitForSeconds(baseDelay);
        }

        yield return new WaitForSeconds(1.2f);

        foreach (GameObject ball in allBalls)
        {
            if (ball != keyBall)
            {
                ball.SetActive(false);
            }
            else
            {
                Renderer renderer = ball.GetComponent<Renderer>();
                renderer.material.color = Color.green;
                renderer.material.SetColor("_EmissionColor", Color.green * 6f);
                ball.transform.localScale = Vector3.one * 2.0f;
            }
        }

        if (foundText != null)
        {
            foundText.text = "FOUND!";
            foundText.gameObject.SetActive(true);
        }

        Debug.Log("Grover search with ripple complete!");
    }
}
