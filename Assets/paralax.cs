using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class paralax : MonoBehaviour
{
    [SerializeField] Material Material;
    [SerializeField] float speed;
    [SerializeField] float yOffset;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(Camera.main.transform.position.x, Camera.main.transform.position.y + yOffset, transform.position.z);
        Material.mainTextureOffset = new Vector2(Material.mainTextureOffset.x + speed * Time.deltaTime, 0);
    }
}
