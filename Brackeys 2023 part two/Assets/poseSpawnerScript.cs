using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poseSpawnerScript : MonoBehaviour
{
    //The scriptableObject poses dragged onto the script
    [SerializeField] public Pose[] poseRequestList;

    //The GameObject poses that are in the scene)
    //[SerializeField] public GameObject[] poseQueue;
    List<GameObject> poseQueue = new List<GameObject>();
    [SerializeField] public int queueCount = 0;
    [SerializeField] public GameObject posePrefab;
    [SerializeField] private float setPoseSpeed;

    [SerializeField] public float spawnDelay = 3f;
    [SerializeField] public float spawnDelayTimer;

    // Start is called before the first frame update
    void Start()
    {
        PosePooling();
        spawnDelayTimer = spawnDelay;
    }

    // Update is called once per frame
    void Update()
    {
        //Timer that determines the spawn rate of the poses.
        spawnDelayTimer -= Time.deltaTime;
        if (spawnDelayTimer <= 0.0f && (queueCount < poseQueue.Count))
        {
            timerEnded();
            spawnDelayTimer = spawnDelay;
        }
    }

    void timerEnded()
    {
        //choose the next pose in order, and set it to active usign a function on it.
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
            GameObject tempPose = Instantiate(posePrefab, transform.position, Quaternion.identity, transform);
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
        }
    }
}
