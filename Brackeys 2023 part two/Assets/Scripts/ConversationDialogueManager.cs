using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class ConversationDialogueManager : MonoBehaviour
{
    [SerializeField] private int NextSceneIndex;
    [SerializeField] private string dialogueAssigner;
    public Dialogue dialogue;

    //List that holds all the dialogue ui text objects
    //[SerializeField] public List<TMP_Text> Dialogue = new List<TMP_Text>();
    [SerializeField] private TMP_Text dialogueText;


    public Queue<string> sentences;

    [SerializeField] private GameObject FadeToBlack;



    [SerializeField] private Image Actor1;
    [SerializeField] private Sprite actor1Base;
    [SerializeField] private Sprite actor1Alt;

    [SerializeField] private Image Actor2;
    [SerializeField] private Sprite actor2Base;
    [SerializeField] private Sprite actor2Alt;



    [SerializeField] private bool fadingIn;
    [SerializeField] private float fadeInCounter = 0f;
    [SerializeField] private bool fadingOut;
    [SerializeField] private float fadeOutCounter = 1f;


    [SerializeField] private GameObject continueButton;
    [SerializeField] private GameObject nextSceneButton;

    [SerializeField] private bool lastText = false;

    [SerializeField] private int nextDialogueIndex = 0;

    //[SerializeField] private int NextSceneIndex;

    // Start is called before the first frame update
    void Start()
    {
        //dialogueText.text = "...";
        Actor1.enabled = true;
        Actor2.enabled = true;
        fadingIn = true;

        Actor1.color = new Color(1, 1, 1, 0.5f);
        Actor2.color = new Color(1, 1, 1, 0.5f);

        sentences = new Queue<string>();
        Debug.Log(sentences.Count);
        Debug.Log("working kinda");
        //DisplayNextSentence();
    }

    // Update is called once per frame
    void Update()
    {
        //For fading in or out
        //Fade in comes from Start(), Fade out comes from "TutorialButton"
        if (fadingOut)
        {
            if (fadeOutCounter <= 1)
            {
                fadeOutCounter += 0.005f;
                FadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, fadeOutCounter);
            }
        }
        if (fadingIn)
        {
            if (fadeInCounter >= 0)
            {
                fadeInCounter -= 0.005f;
                FadeToBlack.GetComponent<Image>().color = new Color(0, 0, 0, fadeInCounter);
            }
            else
            {
                fadingIn = false;
            }
        }
    }

    public void ContinueButton()
    {
        Debug.Log("continueButton Pressed");

        nextDialogueIndex++;

        if (nextDialogueIndex == sentences.Count)
        {
            continueButton.SetActive(false);
            nextSceneButton.SetActive(true);
            Debug.Log("End of dialogue at index: " + nextDialogueIndex);
        }

        DisplayNextSentence();

        Debug.Log("index is: " + nextDialogueIndex);

        //Checks which dialogue sequence this scene is, and uses the appropriate button function

        //Template for editing sprites
        if (dialogueAssigner == "nameOfDialogueSequence")
        {
            if (nextDialogueIndex == 2)
            {
                Actor1.sprite = actor1Base;
                Actor2.enabled = true;
                BothTalking();
            }

            if (nextDialogueIndex == 3)
            {
                Actor1Talking();
            }

            if (nextDialogueIndex == 4)
            {
                Actor2Talking();
            }
        }
        //Intro Scene
        if (dialogueAssigner == "intro")
        {
            if (nextDialogueIndex == 2)
            {
                Actor1.sprite = actor1Base;
                Actor2.enabled = true;
            }

            if (nextDialogueIndex == 3)
            {
            }

            if (nextDialogueIndex == 5)
            {
            }

            if (nextDialogueIndex == 6)
            {
            }

            if (nextDialogueIndex == 7)
            {

            }

            if (nextDialogueIndex == 8)
            {
                Actor2.enabled = false;
                Actor1.sprite = actor1Alt;
            }
        }
    }

    private void BothTalking()
    {
        Actor1.color = new Color(1, 1, 1, 1f);
        Actor2.color = new Color(1, 1, 1, 1f);
    }
    private void Actor1Talking()
    {
        Actor1.color = new Color(1, 1, 1, 1f);
        Actor2.color = new Color(1, 1, 1, 0.5f);
    }
    private void Actor2Talking()
    {
        Actor2.color = new Color(1, 1, 1, 0.5f);
        Actor2.color = new Color(1, 1, 1, 1f);
    }

    //Doesn't necessarily start the tutorial, just moves to the next scene

    public void StartDialogue(Dialogue dialogue)
    {
        sentences.Clear();

        foreach (string sentence in dialogue.sentences)
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

    IEnumerator TypeSentence(string sentence)
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
        Invoke(nameof(NextScene), 1.2f);
        fadingOut = true;
    }

    public void NextScene()
    {
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Debug.Log("NextScene");
    }
}
