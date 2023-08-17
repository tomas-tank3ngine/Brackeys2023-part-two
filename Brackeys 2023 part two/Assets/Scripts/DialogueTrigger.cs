using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private GameObject dManager;
    private DialogueManager dManagerScript;

    private bool inTrigger;

    public Dialogue dialogue;
    public BoxCollider2D triggerArea;

    public void Start()
    {
        triggerArea = gameObject.GetComponent<BoxCollider2D>();
        dManagerScript = dManager.GetComponent<DialogueManager>();
    }

    public void Update()
    {        
        if (inTrigger && Input.GetKeyDown("e"))
        {
            dManagerScript.DisplayNextSentence();
        }
    }

    public void TriggerDialogue()
    {
        dManagerScript.StartDialogue(dialogue);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        inTrigger = true;

        if (collision.gameObject.CompareTag("Player"))
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {

        inTrigger = false;
        if (collision.gameObject.CompareTag("Player"))
        {
            dManagerScript.EndDialogue();
        }
    }
}
