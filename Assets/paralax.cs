using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    [SerializeField] Material Material;
    [SerializeField] float speed;
    [SerializeField] float yOffset;
    [SerializeField] Rigidbody2D player;
    float refCamSize;
    float refScale;

    // Start is called before the first frame update
    void Start()
    {
        refCamSize = Camera.main.orthographicSize;
        refScale = transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + yOffset, transform.position.z);
        Material.mainTextureOffset = new Vector2(Material.mainTextureOffset.x + speed * player.velocity.x * Time.deltaTime, 0);
        float scale = refScale * (Camera.main.orthographicSize / refCamSize);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}
