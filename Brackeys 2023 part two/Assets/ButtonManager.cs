using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour
{
    public Animator JimAnimator;
    public GameObject Button;
    public GameObject TextObj;

    public GameObject EButton;
    public GameObject MButton;
    public GameObject HButton;

    public GameObject CrowdTalking;
    public GameObject SHHH;

    public bool jumpAnimation;
    public bool cannonBallEnterAnimation;

    public void Start()
    {
        if (cannonBallEnterAnimation)
        {
            JimAnimator.SetTrigger("CannonballEnter");
        }
    }
    public void StartButton()
    {
        Invoke(nameof(JumpAnimationStarter), 1f);
        Invoke(nameof(NextScene), 3f);

        CrowdTalking.SetActive(false);
        SHHH.SetActive(true);

        Button.SetActive(false);
        TextObj.SetActive(false);
    }

    public void RedoButton()
    {
        SceneManager.LoadScene("Main Menu");
    }

    public void JumpAnimationStarter()
    {
        JimAnimator.SetTrigger("Jump");
    }

    public void EasyButton()
    {
        Invoke(nameof(EasyScene), 1.4f);
        JimAnimator.SetTrigger("CannonballExit");
        EButton.SetActive(false);
        MButton.SetActive(false);
        HButton.SetActive(false);
    }
    public void MediumButton()
    {
        Invoke(nameof(MediumScene), 1.4f);
        JimAnimator.SetTrigger("CannonballExit");
        EButton.SetActive(false);
        MButton.SetActive(false);
        HButton.SetActive(false);
    }
    public void HardButton()
    {
        Invoke(nameof(HardScene), 1.4f);
        JimAnimator.SetTrigger("CannonballExit");
        EButton.SetActive(false);
        MButton.SetActive(false);
        HButton.SetActive(false);
    }


    public void QuitButton()
    {
        Application.Quit();
    }

    public void NextScene()
    {

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void GameScene()
    {
        SceneManager.LoadScene("Gameplay"); 
    }
    public void EasyScene()
    {
        SceneManager.LoadScene("Easy");
    }
    public void MediumScene()
    {
        SceneManager.LoadScene("Medium");
    }
    public void HardScene()
    {
        SceneManager.LoadScene("Hard");
    }


}
