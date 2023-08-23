using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int depthCounter;
    public int GoalDepth;
    public bool halfwayThere;
    [SerializeField] private ParticleSystem wooshPS;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (depthCounter == GoalDepth)
        {

        }
    }

    public void perfectInput(string inputDirection)
    {
        wooshPS.startSpeed += 1;
        depthCounter += 1;

        if (depthCounter >= (GoalDepth/2) && !halfwayThere)
        {

            halfwayThere = true;
        }

        
    }

    public void missedInput()
    {
        wooshPS.startSpeed -= 1.5f;
        depthCounter -= 1;
    }


}
