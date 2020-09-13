using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class LevelControl : MonoBehaviour
{
    //Set variables for control level include countdown start, pause and menu.
    private int Timer = 0;
    private int CountDownGame = 3;
    private IEnumerator coroutine;
    public Text CountDownTMP;

    //Access to canvas for control menu UI.
    public Transform countingCanvas;
    public Transform pauseMenu;
    public Transform confirmMenu;
    public Transform ResultWindow;
    public Image transition;
    public Animator trans_animation;
    private IEnumerator exitTransition;

    //Access to player object for enable and disable its.
    //While game is countdown disable player script, when finish countdown enable player script.
    public GameObject player1;
    public GameObject player2;

    public AudioSource m_AudioSource;

    // Start is called before the first frame update
    void Start()
    {
        ResultWindow.gameObject.SetActive(false);
        pauseMenu.gameObject.SetActive(false);
        confirmMenu.gameObject.SetActive(false);
        player1.GetComponent<Player>().enabled = false;
        player2.GetComponent<Player>().enabled = false;
        coroutine = CountDownStart(0);
        StartCoroutine(coroutine);
        CountDownTMP.text = " ";
        exitTransition = DelayScene();
        m_AudioSource.Play();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    //IEnumerator for counting down game start.
    private IEnumerator CountDownStart(int CountNumber)
    {
        
        yield return new WaitForSeconds(1);
        Timer += 1;
        CountDownTMP.text = CountDownGame.ToString();
        CountDownGame -= 1;
        print("Wait " + Timer);
        //CountDownTMP.SetText(CountDownGame.ToString());
        yield return new WaitForSeconds(1);
        Timer += 1;
        CountDownTMP.text = CountDownGame.ToString();
        CountDownGame -= 1;
        print("Wait " + Timer);
        //CountDownTMP.SetText(CountDownGame.ToString());
        yield return new WaitForSeconds(1);
        Timer += 1;
        CountDownTMP.text = CountDownGame.ToString();
        CountDownGame -= 1;
        print("Wait " + Timer);
        //CountDownTMP.SetText(CountDownGame.ToString());
        yield return new WaitForSeconds(1);
        Timer += 1;
        CountDownGame -= 1;
        print("Wait " + Timer);
        CountDownTMP.text = "START";
        //CountDownTMP.SetText("START");
        yield return new WaitForSeconds(1);
        CountDownTMP.text = " ";
        //CountDownTMP.SetText(" ");
        player1.GetComponent<Player>().enabled = true;
        player2.GetComponent<Player>().enabled = true;
        countingCanvas.gameObject.SetActive(false);
    }

    public void Pause()
    {
        if (pauseMenu.gameObject.active == false)
        {
            pauseMenu.gameObject.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            pauseMenu.gameObject.SetActive(false);
            Time.timeScale = 1;
        }
    }

    public void Confirm()
    {
        confirmMenu.gameObject.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(exitTransition);
    }

    public void Cancel()
    {
        pauseMenu.gameObject.SetActive(true);
        confirmMenu.gameObject.SetActive(false);
    }

    public void Exit()
    {
        pauseMenu.gameObject.SetActive(false);
        confirmMenu.gameObject.SetActive(true);
    }

    private IEnumerator DelayScene()
    {
        trans_animation.SetBool("FadeOut", true);
        yield return new WaitUntil(() => transition.color.a == 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
