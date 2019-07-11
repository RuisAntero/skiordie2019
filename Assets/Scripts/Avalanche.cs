using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Avalanche : MonoBehaviour
{
    [SerializeField] playerMovement player;
    [SerializeField] float speed;
    [SerializeField] float modSpeed;

    // Update is called once per frame
    void Update()
    {
        modSpeed = Mathf.Clamp(speed + Time.timeSinceLevelLoad*0.05f + Vector2.Distance(transform.position, player.transform.position)* 0.4f, speed, speed*5);
        if (!player.enabled)
        {
            modSpeed = speed * 4;
        }
        transform.position = new Vector3(transform.position.x + (modSpeed * Time.deltaTime), transform.position.y, transform.position.z);
        if (player.transform.position.y < transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, Mathf.Lerp(transform.position.y, player.transform.position.y, 0.03f), transform.position.z);
        }
    }
}
