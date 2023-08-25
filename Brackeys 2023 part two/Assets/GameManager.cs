using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public int depthCounter;
    public int GoalDepth;
    public bool halfwayThere;
    [SerializeField] private ParticleSystem wooshPS;
    public string currentScene;
    // Start is called before the first frame update
    void Start()
    {
        currentScene = SceneManager.GetActiveScene().name;
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
        

        if (currentScene == "Easy")
        {
            depthCounter += 1;
        }
        /*
        if (currentScene == "Medium")
        {
            depthCounter += 2;
        }
        if (currentScene == "Hard")
        {
            depthCounter += 3;
        }*/        
    }

    public void missedInput()
    {
        wooshPS.startSpeed -= 1.5f;
        //depthCounter -= 1;
    }


}
