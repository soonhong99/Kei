using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class IntroNPC : Collidable
{
    public GameObject dialoguePanel;
    public Text dialogueText;
    public string[] dialogue;
    private int index;

    public float wordSpeed;
    public bool playerIsClose;
    public GameObject contButton;

    protected override void Start()
    {
        dialogueText.text = "";
    }
    protected override void Update()
    {
        // e키와 가까워졌을 때 말을 걸 수 있다.
        if (Input.GetKeyDown(KeyCode.E) && playerIsClose && dialoguePanel != null)
        {
            if (dialoguePanel.activeInHierarchy)
            {
                zeroText();
            }
            else
            {
                dialoguePanel.SetActive(true);
                StopAllCoroutines();
                StartCoroutine(Typing());
            }
        }

        if (dialogueText != null && dialogue != null)
        {
            if (dialogueText.text == dialogue[index])
            {
                contButton.SetActive(true);
            }
        }

    }

    public void zeroText()
    {
        //dialogueText.text = "";
        //index = 0;
        //dialoguePanel.SetActive(false);
        if (dialoguePanel.activeSelf && dialogueText.text != "" && dialogueText.text != null && dialogue != null)
        {
            dialogueText.text = "";
            index = 0;
            dialoguePanel.SetActive(false);
            StopAllCoroutines();
        }
    }

    IEnumerator Typing()
    {
        foreach(char letter in dialogue[index].ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSeconds(wordSpeed);
        }
    }

    public void NextLine()
    {
        contButton.SetActive(false);

        if (index < dialogue.Length - 1)
        {
            index++;
            dialogueText.text = "";
            StopAllCoroutines();
            StartCoroutine(Typing());
        }
        else
        {
            zeroText() ;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Fighter"))
        {
            playerIsClose = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Fighter"))
        {
            playerIsClose = false;
            zeroText();
        }
    }

}
