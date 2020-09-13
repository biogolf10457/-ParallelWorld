using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public Transform tutorialCanvas;

    public Image transition;
    public Animator trans_animation;
    public Animator loading;

    private IEnumerator coroutine;

    public GameObject loading_text;
    public AudioSource m_AudioSource;

    void Start()
    {
        tutorialCanvas.gameObject.SetActive(false);
        loading_text.SetActive(false);
        m_AudioSource.Play();
    }

    void Play()
    {
        coroutine = DelayScene();
        StartCoroutine(coroutine);
    }

    void Exit()
    {
        Application.Quit();
    }

    public void Tutorial() {
        tutorialCanvas.gameObject.SetActive(true);
    }

    public void Close() {
        tutorialCanvas.gameObject.SetActive(false);
    }

    private IEnumerator DelayScene()
    {
        trans_animation.SetBool("FadeOut", true);
        loading.SetBool("Loading", true);
        loading_text.SetActive(true);
        yield return new WaitUntil(()=> transition.color.a==1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
