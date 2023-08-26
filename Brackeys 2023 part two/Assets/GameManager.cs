using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public GameObject spawner;
    public poseSpawnerScript spawnerScript;
    public Animator HighscoreAnim;
    public Animator ScoreAnim;
    public static int depthCounter;
    public static int Highscore;

    public int GoalDepth;
    public bool halfwayThere;
    public string currentScene;

    public TMP_Text depthText;
    public TMP_Text highscoreText;

    // Start is called before the first frame update
    void Start()
    {
        if (spawner != null)
        {
            spawnerScript = spawner.GetComponent<poseSpawnerScript>();
        }
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            Highscore = PlayerPrefs.GetInt("ppHighScore", Highscore);
        }

        if (SceneManager.GetActiveScene().name == "Gameplay")
        {
            depthCounter = 0;
        }

        currentScene = SceneManager.GetActiveScene().name;
        if (depthText != null)
        {
            depthText.text = depthCounter.ToString() + "ft";
        }
        if (highscoreText != null)
        {
            highscoreText.text = Highscore.ToString() + "ft";
        }
            
        /*
        if ((HighscoreAnim != null && ScoreAnim != null))
        {
            HighscoreAnim.SetTrigger;
        }*/
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void perfectInput(string inputDirection)
    {
        depthCounter += 1;
        depthText.text = depthCounter + "ft";
    }

    public void missedInput()
    {
        //depthCounter -= 1;
        spawnerScript.DeleteAllPoses();

        Invoke(nameof(loadEndScene), 1.2f);
        if (depthCounter > Highscore)
        {
            Highscore = depthCounter;
            PlayerPrefs.SetInt("ppHighScore", Highscore);
        }
    }

    public void loadEndScene()
    {
        SceneManager.LoadScene("endScene");
    }
}
