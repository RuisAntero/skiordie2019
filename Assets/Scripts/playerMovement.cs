using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float groundDistance;
    bool CanJump()
    {
        int layermask = 1 << 8;
        if (Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), Color.green, 1);
            return true;
        }
        else if (Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(.5f, -groundDistance, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(.5f, -groundDistance, 0), Color.green, 1);
            return true;
        }
        else if (Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-.5f, -groundDistance, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-.5f, -groundDistance, 0), Color.green, 1);
            return true;
        }
        else
        {
            return false;
        }
    }

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
        
        
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (CanJump())
            {
                body.AddForce(new Vector2(0, 2000));
                body.AddTorque(45f);
            }
        }

        if (Input.GetKey(KeyCode.S))
        {
            if (CanJump())
            {
                body.AddForce(new Vector2 (1,0) * Time.deltaTime*2000f);
            }
            else
            {
                body.AddForce(new Vector2(0, -1) * Time.deltaTime*2000f);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            body.velocity = body.velocity / 2;
        }
    }
}
