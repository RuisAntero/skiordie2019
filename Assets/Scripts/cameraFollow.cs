using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Rigidbody2D player;
    float refSize;

    // Start is called before the first frame update
    void Start()
    {
        refSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Vector3 endPos = Vector3.Lerp(transform.position, player.transform.position + new Vector3( 7 + Mathf.Pow(player.velocity.x*0.1f,3), -player.velocity.x*0.5f, 0), 0.05f);
        endPos.z = -10;
        transform.position = endPos;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, refSize + player.velocity.x * 0.3f, 0.02f);
    }
}
