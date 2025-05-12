using UnityEngine;
using System.Collections;
using TMPro;

public class DialogueLVL2 : MonoBehaviour
{

    public TextMeshProUGUI textComponent;
    public GameObject canvas;
    public float TextSpeed;
    public bool canChat = false;
    public bool ChatStarted = false;
    public DoorController doorController;


    private int index = 0;
    public string[] lines;
    //-------------------------------------------------------------
    void Start()
    {
        textComponent.text = string.Empty;

    }

    // Update is called once per frame
    void Update()
    {
        if (canChat)
        {
            if (Input.GetKeyDown(KeyCode.E) && ChatStarted == false)
            {
                canChat = false;
                canvas.SetActive(true);
                StartDialogue();
                this.GetComponent<SphereCollider>().enabled = false;
                ChatStarted = true;
            }
        }

        if (Input.GetMouseButtonDown(0) && ChatStarted)
        {
            if (textComponent.text == lines[index])
            {
                NextLine();
            }
            else
            {
                StopAllCoroutines();
                textComponent.text = lines[index];
            }
        }

    }
    //-------------------------------------------------------------
    public void StartDialogue()
    {
        if (ChatStarted == false)
        {
            index = 0;
            textComponent.text = string.Empty;
            canvas.SetActive(true);
            StartCoroutine(TypeLine());
            ChatStarted = true;
        }
    }
    //-------------------------------------------------------------
    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            canChat = true;
        }
        else
        {
            canChat = false;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canChat = false;
    }
    //-------------------------------------------------------------
    IEnumerator TypeLine()
    {
        foreach (char c in lines[index].ToCharArray())
        {
            textComponent.text += c;
            yield return new WaitForSeconds(TextSpeed);
        }
    }
    //-------------------------------------------------------------
    void NextLine()
    {
        if (index < lines.Length - 1)
        {
            index++;
            textComponent.text = string.Empty;
            StartCoroutine(TypeLine());

            doorController.OpenDoor();
        }
        else
        {
            ChatStarted = false;
            canvas.SetActive(false);
            index = 0;
        }
    }
}
