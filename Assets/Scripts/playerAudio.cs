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
        if (player.Grounded(false))
        {
            targetVolume = refVolume * player.GetComponent<Rigidbody2D>().velocity.magnitude / 28;
        }
        else
        {
            targetVolume = 0f;
        }
        if (!player.enabled)
        {
            targetVolume = 0f;
        }
        skiAudio.volume = Mathf.Lerp(skiAudio.volume, targetVolume, 0.3f); 
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
