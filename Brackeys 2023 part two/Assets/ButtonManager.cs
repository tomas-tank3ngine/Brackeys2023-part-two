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
        JimAnimator.SetTrigger("Jump"); 

        Invoke(nameof(NextScene), 1.4f);

        Button.SetActive(false);
        TextObj.SetActive(false);
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
