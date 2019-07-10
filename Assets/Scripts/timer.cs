using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class timer : MonoBehaviour
{
    [SerializeField] GameData gameData;
    [SerializeField] TextMeshProUGUI text;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int minutes = Mathf.FloorToInt(gameData.timeElapsed / 60F);
        int seconds = Mathf.FloorToInt(gameData.timeElapsed - minutes * 60);
        string niceTime = string.Format("{0:0}:{1:00}", minutes, seconds);

        text.text = niceTime;
    }
}
