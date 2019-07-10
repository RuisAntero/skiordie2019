using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAudio : MonoBehaviour
{

    [SerializeField] AudioSource skiAudio;
    [SerializeField] AudioSource jumpAudio;
    [SerializeField] AudioSource hitAudio;
    playerMovement player;
    float refVolume;
    float targetVolume;

    // Start is called before the first frame update
    void Start()
    {
        refVolume = skiAudio.volume;
        skiAudio.volume = 0;
        player = GetComponent<playerMovement>();
    }

    private void Update()
    {
        skiAudio.volume = Mathf.Lerp(skiAudio.volume, targetVolume, 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (player.Grounded(false))
        {
            targetVolume = refVolume;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (!player.Grounded(false))
        {
            targetVolume = 0;
        }
    }

    public void playJump()
    {
        jumpAudio.Play();
    }

    public void playHit()
    {
        hitAudio.Play();
    }
}
