﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Endscreen : MonoBehaviour
{
    [SerializeField] playerMovement player;
    [SerializeField] CanvasGroup canvasGroup;
    float countdown = 2f;

    // Update is called once per frame
    void Update()
    {
        if (!player.enabled)
        {
            countdown -= Time.deltaTime;
            if (countdown <= 0)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 0.02f);
                canvasGroup.interactable = true;
            }
        }
    }

    public void Retry()
    {
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        SceneManager.LoadScene("Menu");
    }
}
