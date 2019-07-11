using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panelCredits;
    [SerializeField] AudioClip soundButton;
    AudioSource audioSrc;

    void Awake() {
        audioSrc = GetComponent<AudioSource>();
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        //Debug.Log(name + " Game Object Clicked!");
        if (panelCredits.activeSelf)
        {
            CloseCredits();
        }
    }

    public void Play()
    {
        PlayButtonSound();
        SceneManager.LoadScene("Game");
    }

    public void OpenCredits()
    {
        PlayButtonSound();
        panelCredits.SetActive(true);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
    }

    public void Quit()
    {
        PlayButtonSound();
        Application.Quit();
    }

    void PlayButtonSound()
    {
        audioSrc.PlayOneShot(soundButton, 0.5f);
    }
}
