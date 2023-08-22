using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerScript : MonoBehaviour
{
    public GameObject perfectPose;
    public GameObject imperfectPose;
    
    public EdgeCollider2D InputZone;

    public string desiredInput;
    public string currentInput;
    private GameObject currentPose;

    private bool inPerfect;
    private bool inLate;


    private bool softInputDisabled;
    private float softDisableTimer;

    private bool inputDisabled;
    private float disableTimer;


    // Start is called before the first frame update
    void Start()
    {
        InputZone = GetComponent<EdgeCollider2D>();
        //imperfectPoseHitbox = imperfectPose.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {

        if (softInputDisabled)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.6f);
            softDisableTimer -= Time.deltaTime;
            if (softDisableTimer <= 0.0f)
            {
                softInputDisabled = false;
                InputZone.enabled = true;
            }
        }

        if (inputDisabled)
        {
            GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0.2f);
            disableTimer -= Time.deltaTime;
            if (disableTimer <= 0.0f)
            {
                inputDisabled = false;
                InputZone.enabled = true;
            }
        }
        if (inPerfect || inLate && !(inputDisabled || softInputDisabled))
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                //DirectionChecker() returns a string of whatever key the player has pressed.
                if (DirectionChecker() == desiredInput)
                {
                    if (inPerfect)
                    {
                        softInputDisabled = true;
                        softDisableTimer = 0.05f;
                        Debug.Log("Perfect input");
                    }

                    if (inLate)
                    {
                        softInputDisabled = true;
                        softDisableTimer = 0.05f;
                        Debug.Log("Late input");
                    }
                }
            }
        }

        if (!(inPerfect || inLate) && !(inputDisabled || softInputDisabled))
        {
            if (Input.GetAxisRaw("Vertical") != 0 || Input.GetAxisRaw("Horizontal") != 0)
            {
                inputDisabled = true;
                InputZone.enabled = false;
                disableTimer = 0.5f;
                Debug.Log("Missedinput");
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("perfectBox") )
        {
            desiredInput = collision.GetComponent<PoseScript>().poseIdentity.keyInput;
            inPerfect = true;
        }
        if (collision.CompareTag("lateBox"))
        {
            inLate = true;
        }
    }

    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("perfectBox"))
        {
            inPerfect = false;
        }

        if (collision.CompareTag("lateBox"))
        {
            inLate = false;
            desiredInput = "none";
        }
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
}
