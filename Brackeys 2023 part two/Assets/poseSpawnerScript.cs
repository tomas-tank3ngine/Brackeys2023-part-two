using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseSpawnerScript : MonoBehaviour
{
    //The scriptableObject poses dragged onto the script
    public bool usingPool;
    [SerializeField] public Pose[] PoseBank;

    [SerializeField] public Pose[] poseRequestList;

    //The GameObject poses that are in the scene)
    //[SerializeField] public GameObject[] poseQueue;
    List<GameObject> poseQueue = new List<GameObject>();
    [SerializeField] public int queueCount = 0;
    [SerializeField] public GameObject posePrefab;
    [SerializeField] private float setPoseSpeed;

    [SerializeField] public float spawnDelay = 1f;
    [SerializeField] public float spawnDelayTimer;

    [SerializeField] private bool levelActive = false;
    [SerializeField] private GameObject TutorialScreen;
    [SerializeField] private GameObject SpawnPosition;
    public static bool tutorialSeen = false;

    public int spawnOrder;

    public bool tripletActive;
    public int comboCount;

    // Start is called before the first frame update
    void Start()
    {
        PosePooling();
        spawnDelayTimer = spawnDelay;
        if (tutorialSeen)
        {
            //removes the tutorial overlay in the playgame function.
            PlayGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Timer that determines the spawn rate of the poses.
        if (levelActive)
        {
            spawnDelayTimer -= Time.deltaTime;
            if (spawnDelayTimer <= 0.0f && (queueCount < poseQueue.Count))
            {
                if (usingPool)
                {
                    poolTimerEnded();
                    spawnDelayTimer = spawnDelay;
                }
                if (!usingPool)
                {
                    randomSpawnTimerEnded();

                    if (spawnOrder % 10 == 0)
                    {
                        Debug.Log("Speed Increased");
                        setPoseSpeed += 0.5f;
                    }

                    if (!(comboCount > 0) && Random.Range(1, 101) <= 25)
                    {
                        comboCount = Random.Range(2,4);
                        spawnDelay = 0.5f;
                    }
                    if (comboCount <= 0)
                    {
                        spawnDelay = 1f;
                    }
                    if (comboCount > 0)
                    {
                        spawnDelay = 0.5f;
                        comboCount--;
                    }
                    spawnDelayTimer = spawnDelay;
                }
            }
        }
    }

    public void triplet()
    {

    }
    public void randomSpawnTimerEnded()
    {
        //generate random number:
        var randPoseIndex = Random.Range(0, 5);
        Debug.Log("Pose: " + randPoseIndex);
        GameObject tempPose = Instantiate(posePrefab, SpawnPosition.transform.position, Quaternion.identity, transform);
        var tempPoseScript = tempPose.GetComponent<PoseScript>();

        //set the speed of each instantiated object to be the set speed of this level.
        tempPoseScript.speed = setPoseSpeed;

        //Selects a random pose from the bank and gives this its identity.
        
        tempPoseScript.poseIdentity = PoseBank[randPoseIndex];

        //Set the spawnOrder of each item, then raise the tally of the spawnOrder.
        tempPoseScript.spawnOrder = spawnOrder;
        tempPoseScript.ActivatePose();
        spawnOrder++;

        tempPose.GetComponent<SpriteRenderer>().sprite = tempPoseScript.poseIdentity.sprite;
        
        poseQueue.Add(tempPose);
    }

    void poolTimerEnded()
    {
        //choose the next pose in order, and set it to active using a function on it.
        //Once set active, the script on that pose will automatically have it moving upwards on Start().
        poseQueue[queueCount].GetComponent<PoseScript>().ActivatePose();
        queueCount++;
    }

    public void PosePooling()
    {
        //Sketchy object pooling
        int spawnOrder = 0;
        foreach (var item in poseRequestList)
        {
            GameObject tempPose = Instantiate(posePrefab, SpawnPosition.transform.position, Quaternion.identity, transform);
            var tempPoseScript = tempPose.GetComponent<PoseScript>();

            //set the speed of each instantiated object to be the set speed of this level.
            tempPoseScript.speed = setPoseSpeed;

            //Reads the current item (aka a pose in an array) and reads the info fields on the scriptableobject.
            tempPoseScript.poseIdentity = item;

            //Set the spawnOrder of each item, then raise the tally of the spawnOrder.
            tempPoseScript.spawnOrder = spawnOrder;
            spawnOrder++;

            tempPose.GetComponent<SpriteRenderer>().sprite = item.sprite;

            poseQueue.Add(tempPose);

            //The item sets sets the "isActive" boolean to false on start().
            tempPoseScript.DeactivatePose();
        }
    }

    public void PlayGame()
    {
        TutorialScreen.SetActive(false);
        tutorialSeen = true;
        playCountdown();
        Invoke(nameof(activateLevel), 3.5f);
    }
    public void playCountdown()
    {
        GetComponent<Animator>().SetTrigger("StartCountdown");
    }

    public void activateLevel()
    {
        levelActive = true;
        GetComponent<SpriteRenderer>().enabled = false;
    }

    public void DeleteAllPoses()
    {
        Debug.Log("poses deleted");
        levelActive = false;
        foreach (var pose in poseQueue)
        {
            //poseQueue.Remove(pose);
            Destroy(pose);
        }
    }
}
