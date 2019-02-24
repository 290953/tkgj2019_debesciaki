using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class DialogueManager : MonoBehaviour
{

    public GameObject uiDialogue;
    public Text nameText;
    public Text dialogueText;

    private Queue<Sentence> sentences;

    void Start()
    {
        sentences = new Queue<Sentence>();
    }

    public void StartDialogue(Dialogue dialogue)
    {
        //nameText.text = dialogue.name;
        sentences.Clear();

        foreach (Sentence sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if (sentences.Count == 0)
        {
            EndDialogue();
            return;
        }

        Sentence sentence = sentences.Dequeue();
        StopAllCoroutines();
        nameText.text = sentence.name;
        StartCoroutine(TypeSentence(sentence.text));
    }

    IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
        yield return new WaitForSeconds(2f);
        DisplayNextSentence();
    }

    void EndDialogue()
    {
        uiDialogue.SetActive(false);
        SceneManager.LoadScene("Forest");
    }
}
