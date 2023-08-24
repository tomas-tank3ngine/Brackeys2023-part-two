using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    [SerializeField] public Sprite[] poseSprites;
    [SerializeField] private SpriteRenderer divermanSR;
    [SerializeField] private Sprite currentSprite;

    public GameObject perfectPose;
    public GameObject imperfectPose;
    
    public CircleCollider2D InputZone;

    public string desiredInput;
    public string currentInput;
    private GameObject currentPose;

    private bool NoteDetected;
    private GameObject currentNote;
    private GameObject gameManager;
    private GameManager GMScript;


    private bool softInputDisabled;
    private float softDisableTimer;

    private bool inputDisabled;
    private float disableTimer;


    // Start is called before the first frame update
    void Start()
    {
        InputZone = GetComponent<CircleCollider2D>();
        gameManager = GameObject.FindGameObjectWithTag("GM");
        GMScript = gameManager.GetComponent<GameManager>();

        divermanSR = GetComponent<SpriteRenderer>();
        currentSprite = divermanSR.sprite;
        //imperfectPoseHitbox = imperfectPose.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (softInputDisabled)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.9f);
            softDisableTimer -= Time.deltaTime;
            if (softDisableTimer <= 0.0f)
            {
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                softInputDisabled = false;
                InputZone.enabled = true;
            }
        }

        if (inputDisabled)
        {
            GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0.2f);
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0.0f)
            {
                GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 1f);
                inputDisabled = false;
                InputZone.enabled = true;
            }
        }


        if (!(inputDisabled || softInputDisabled))
        {
            currentInput = DirectionChecker();

            switchSprite(currentInput);


            /*
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                currentInput = DirectionChecker();


                //DirectionChecker() returns a string of whatever key the player has pressed.
                if (DirectionChecker() == desiredInput)
                {
                    if (NoteDetected)
                    {
                        //Got the note
                        softInputDisabled = true;
                        softDisableTimer = 0.05f;
                        Debug.Log("Perfect Input!");
                        currentNote.GetComponent<PoseScript>().GotPose();
                        //currentNote = null;

                        GMScript.perfectInput(desiredInput);


                        //todo animation

                        switchSprite(desiredInput);
                    }
                }
            }
            
        }

        if (!NoteDetected && !(inputDisabled || softInputDisabled))
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                inputDisabled = true;
                InputZone.enabled = false;
                disableTimer = 0.5f;

                GMScript.missedInput();
                switchSprite("missed");
                Invoke(nameof(resetSprite), 0.25f);
                Debug.Log("Missedinput");
            }
        }
            */
        }
    }
        public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Note"))
        {
            currentNote = collision.gameObject;
            desiredInput = collision.GetComponent<PoseScript>().poseIdentity.keyInput;
            NoteDetected = true;
            if (currentInput == desiredInput)
            {
                softInputDisabled = true;
                softDisableTimer = 0.05f;
                Debug.Log("Perfect Input!");

                collision.GetComponent<PoseScript>().GotPose();
                collision.GetComponent<PoseScript>().alreadyHit = true;
                //currentNote = null;

                GMScript.perfectInput(desiredInput);
                return;
            }

            if (currentInput != desiredInput)
            {
                inputDisabled = true;
                InputZone.enabled = false;
                disableTimer = 0.5f;

                GMScript.missedInput();
                switchSprite("missed");
                Invoke(nameof(resetSprite), 0.4f);
                Debug.Log("Missedinput");

                collision.GetComponent<PoseScript>().MissedPose();
                collision.GetComponent<PoseScript>().alreadyHit = true;
            }
            /*
            if (currentInput != desiredInput)
            {
                softInputDisabled = true;
                softDisableTimer = 0.05f;
                Debug.Log("Perfect Input!");
                currentNote.GetComponent<PoseScript>().GotPose();
                //currentNote = null;

                GMScript.perfectInput(desiredInput);
            }*/

        }
        /*
        if (collision.CompareTag("Note") )
        {
            Debug.Log("hitbox entered");
            currentNote = collision.gameObject;
            desiredInput = collision.GetComponent<PoseScript>().poseIdentity.keyInput;
            NoteDetected = true;


        }*/
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        /*
        if (collision.CompareTag("Note"))
        {
            desiredInput = null;
            if (collision.GetComponent<PoseScript>().alreadyHit != true)
            {
                inputDisabled = true;
                InputZone.enabled = false;
                disableTimer = 0.5f;

                GMScript.missedInput();
                switchSprite("missed");
                Invoke(nameof(resetSprite), 0.4f);
                collision.GetComponent<PoseScript>().MissedPose();
                Debug.Log("Missedinput On Exit");
            }
            currentNote = null;
            NoteDetected = false;
        }*/
    }

    public string DirectionChecker()
    {
        if (Input.GetAxisRaw("Horizontal") == 1)
        {
            //right
            string registeredInput = "right";
            return registeredInput;
        }
        if (Input.GetAxisRaw("Horizontal") == -1)
        {
            //left
            string registeredInput = "left";
            return registeredInput;
        }
        if (Input.GetAxisRaw("Vertical") == 1)
        {
            //up
            string registeredInput = "up";
            return registeredInput;
        }
        if (Input.GetAxisRaw("Vertical") == -1)
        {
            //down
            string registeredInput = "down";
            return registeredInput;
        }
        string noInput = "none";
        return noInput;
    }

    public void switchSprite(string inputDirection)
    {
        if (inputDirection == "none")
        {
            divermanSR.sprite = poseSprites[0];
        }
        if (inputDirection == "up")
        {
            divermanSR.sprite = poseSprites[1];
        }
        if (inputDirection == "down")
        {
            divermanSR.sprite = poseSprites[2];
        }
        if (inputDirection == "left")
        {
            divermanSR.sprite = poseSprites[3];
        }
        if (inputDirection == "right")
        {
            divermanSR.sprite = poseSprites[4];
        }
        if (inputDirection == "missed")
        {
            divermanSR.sprite = poseSprites[5];
        }
    }
    public void resetSprite()
    {
        divermanSR.sprite = poseSprites[0];
    }
}
