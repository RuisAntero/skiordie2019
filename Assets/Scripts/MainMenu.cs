using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;


public class MainMenu : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] GameObject panelCredits;

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
        SceneManager.LoadScene("Game");
    }

    public void OpenCredits()
    {
        panelCredits.SetActive(true);
    }

    public void CloseCredits()
    {
        panelCredits.SetActive(false);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
