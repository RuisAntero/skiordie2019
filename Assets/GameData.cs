using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameData : MonoBehaviour
{
    public float timeElapsed;
    [SerializeField] playerMovement player;

    private void Update()
    {
        
        if (player.enabled)
        {
            timeElapsed += Time.deltaTime;
        }
    }
}
