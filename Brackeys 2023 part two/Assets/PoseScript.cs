using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoseScript : MonoBehaviour
{
    [SerializeField] public bool isActive;
    [SerializeField] public Pose poseIdentity;
    [SerializeField] public int spawnOrder;
    public float speed;
    private GameObject PoseSpawner;

    [SerializeField] private GameObject LateHolder;
    [SerializeField] private GameObject PerfectHolder;

    // Start is called before the first frame update
    void Start()
    {
        PoseSpawner = GameObject.FindGameObjectWithTag("Spawner");
        DeactivatePose();
        //GetComponent<SpriteRenderer>().sprite = poseIdentity.sprite;
    }

    // Update is called once per frame
    void Update()
    {
        if (isActive)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y + speed * Time.deltaTime);
        }

        if (transform.position.y > 5)
        {
            DeactivatePose();
        }
    }

    public void ActivatePose()
    {
        GetComponent<SpriteRenderer>().enabled = true;
        isActive = true;
    }

    public void DeactivatePose()
    {
        GetComponent<SpriteRenderer>().enabled = false;
        isActive = false;
    }
}
