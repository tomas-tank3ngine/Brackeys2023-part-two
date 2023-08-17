using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class DialogueManager : MonoBehaviour
{
    //public Text nameText;
    //public Text dialogueText;

    [SerializeField] private bool showName;
    [SerializeField] private bool isClosable;
    [SerializeField] private BoxCollider2D TriggerAreaBC2D;
    public TMP_Text nameText;
    [SerializeField] private TMP_Text dialogueText;
    

    public Animator animator;


    public Queue<string> sentences;

    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        animator.SetBool("isOpen", true);

        if (showName)
        {
            nameText.text = dialogue.name;
        }

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
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

        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
        //dialogueText.text = sentence;
    }

    IEnumerator TypeSentence (string sentence)
    {
        dialogueText.text = "";
        foreach (char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    public void EndDialogue()
    {
        if (isClosable)
        {
            animator.SetBool("isOpen", false);
        }
        else
        {
            TriggerAreaBC2D.enabled = false;
        }
    }
}
