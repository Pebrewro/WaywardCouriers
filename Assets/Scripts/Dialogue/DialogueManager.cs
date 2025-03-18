using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI dialogueText;
    public Image characterImage;

    public Animator anim; //DialogueBox
    public Animator anim2; //Char Sprite
    public Animator anim3; //Optional quest UI

    public static bool IsDialogueActive { get; private set; }

    private Queue<string> sentences;

    private bool hasQuest;

    private void Start()
    {
        sentences = new Queue<string>();
        IsDialogueActive = false;
    }

    public void StartDialogue(Dialogue dialogue)
    {
        anim.SetBool("isOpen", true);

        Debug.Log("Starting conversation with " + dialogue.name);
        IsDialogueActive = true;
        Time.timeScale = 0;

        nameText.text = dialogue.name;

        sentences.Clear();

        if (dialogue.characterSprite != null)
        {
            anim2.SetBool("isHere", true);
            characterImage.sprite = dialogue.characterSprite;
            characterImage.enabled = true;
        }
        else
        {
            characterImage.enabled = false;
            anim2.SetBool("isHere", false);
        }

        foreach (string sentence in dialogue.sentences) 
        {
            sentences.Enqueue(sentence);
        }

        hasQuest = dialogue.hasQuest;

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        string sentence = sentences.Dequeue();

        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        Debug.Log(sentence);
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return new WaitForSecondsRealtime(0.05f);
        }
    }
    void EndDialogue()
    {
        Debug.Log("End of conversation.");
        anim.SetBool("isOpen", false);
        anim2.SetBool("isHere", false);
        characterImage.enabled = false;
        IsDialogueActive = false;
        Time.timeScale = 1;

        if (hasQuest)
        {
            anim3.SetBool("isQuest", true);
        }
    }
}
