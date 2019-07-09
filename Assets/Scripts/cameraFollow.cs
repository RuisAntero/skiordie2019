using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraFollow : MonoBehaviour
{
    public Rigidbody2D player;
    float refSize;
    Vector3 refPos;

    // Start is called before the first frame update
    void Start()
    {
        refPos = transform.position - player.transform.position;
        refSize = Camera.main.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position + refPos;
        Camera.main.orthographicSize = Mathf.Lerp(Camera.main.orthographicSize, refSize + player.velocity.magnitude * 0.5f, 0.02f);
    }
}
