using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float groundDistance;
    bool canJump = false;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (body.velocity.magnitude > maxSpeed)
        {
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
        }
        
        
        if (Input.GetKeyDown(KeyCode.W) && canJump)
        {
            int layermask = 1 << 8;
            if (Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), groundDistance, layermask))
            {
                Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), Color.green, 1);
                body.AddForce(new Vector2(0, 1000));
                body.AddTorque(45f);
                canJump = false;
            }
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.layer == 8)
        {
            canJump = true;
        }
    }
}
