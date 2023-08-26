using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static int depthCounter;
    public static int Highscore;

    public int GoalDepth;
    public bool halfwayThere;
    [SerializeField] private ParticleSystem wooshPS;
    public string currentScene;

    public TMP_Text depthText;

    // Start is called before the first frame update
    void Start()
    {
        
        currentScene = SceneManager.GetActiveScene().name;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void perfectInput(string inputDirection)
    {
        wooshPS.startSpeed += 1;
        
        depthCounter += 1;
        depthText.text = depthCounter + "ft";
    }

    public void missedInput()
    {
        wooshPS.startSpeed -= 1.5f;
        //depthCounter -= 1;

        Invoke(nameof(endScene), 1.2f);
        if (depthCounter > Highscore)
        {
            Highscore = depthCounter;
            PlayerPrefs.SetInt("ppHighScore", Highscore);
        }
    }

    public void endScene()
    {
        SceneManager.LoadScene("endScene");
    }
}
