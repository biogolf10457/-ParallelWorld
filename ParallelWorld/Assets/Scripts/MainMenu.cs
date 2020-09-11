using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

    public Image transition;
    public Animator trans_animation;
    public Animator loading;

    private IEnumerator coroutine;

    public GameObject loading_text;

    void Start()
    {
        loading_text.SetActive(false);
    }

    void Play()
    {
        coroutine = DelayScene();
        loading_text.SetActive(true);
        StartCoroutine(coroutine);
    }

    void Exit()
    {
        Application.Quit();
    }

    private IEnumerator DelayScene()
    {
        trans_animation.SetBool("FadeOut", true);
        loading.SetBool("Loading", true);
        yield return new WaitUntil(()=> transition.color.a==1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
