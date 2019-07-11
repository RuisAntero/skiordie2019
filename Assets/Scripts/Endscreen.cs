using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class Endscreen : MonoBehaviour
{
    [SerializeField] playerMovement player;
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] GameData GameData;
    [SerializeField] TextMeshProUGUI highscore;
    float countdown = 2f;

    // Update is called once per frame
    void Update()
    {
        if (!player.enabled)
        {
            countdown -= Time.deltaTime;
            if (GameData.timeElapsed > GameData.HighScore())
            {
                PlayerPrefs.SetFloat("HighScore", GameData.timeElapsed);
            }
            if (countdown <= 0)
            {
                canvasGroup.alpha = Mathf.Lerp(canvasGroup.alpha, 1, 0.02f);
                canvasGroup.interactable = true;

                int minutes = Mathf.FloorToInt(GameData.HighScore() / 60F);
                int seconds = Mathf.FloorToInt(GameData.HighScore() - minutes * 60);
                string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);
                highscore.text = "High Score: " + niceTime;

                if (Input.GetKeyDown(KeyCode.Space))
                {
                    Retry();
                }
                
            }
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Quit();
        }
    }

    public void Retry()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("Game");
    }

    public void Quit()
    {
        GetComponent<AudioSource>().Play();
        SceneManager.LoadScene("MainMenu");
    }
}
