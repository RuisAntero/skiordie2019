using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AvalancheOverlay : MonoBehaviour
{

    [SerializeField] GameObject avalanche;
    [SerializeField] GameObject player;
    [SerializeField] Image image;
    float intensity;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        intensity = Mathf.Pow(Mathf.Clamp(1 - ((player.transform.position.x - avalanche.transform.position.x) / 40),0,1), 2);
        image.color = new Color(image.color.r, image.color.g, image.color.b, intensity);
    }
}
