using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{

    [SerializeField] AudioSource audio;
    playerMovement player;
    float refVolume;

    // Start is called before the first frame update
    void Start()
    {
        refVolume = audio.volume;
        audio.volume = 0;
        player = GetComponent<playerMovement>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.CanJump())
        {
            audio.volume = refVolume;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!player.CanJump())
        {
            audio.volume = 0;
        }
    }
}
