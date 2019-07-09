using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float groundDistance;
    [SerializeField] Animator animator;
    [SerializeField] float stunDuration;
    float stunCountdown;
    bool dead;



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

        stunCountdown -= Time.deltaTime;
        if (stunCountdown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                if (CanJump())
                {
                    body.AddForce(new Vector2(0, 2000));
                    body.AddTorque(45f);
                    animator.SetTrigger("Jump");
                    animator.SetBool("Squat", false);
                }
            }

            if (Input.GetKey(KeyCode.S))
            {
                if (CanJump())
                {
                    body.AddForce(new Vector2(1, 0) * Time.deltaTime * 2000f);
                    animator.SetBool("Squat", true);
                }
                else
                {
                    body.AddForce(new Vector2(0, -1) * Time.deltaTime * 2000f);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Squat", false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Obstacle")
        {
            body.velocity = body.velocity / 4;
            animator.SetTrigger("Stun");
            animator.SetBool("Squat", false);
            stunCountdown = stunDuration;
        }
        if (collision.tag == "Finish")
        {
            body.velocity = Vector2.zero;
            stunCountdown = stunDuration;
            dead = true;
            SceneManager.LoadScene("Game");
        }
    }
}
