using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    Rigidbody2D body;
    [SerializeField] float maxSpeed = 0;
    [SerializeField] float groundDistance;
    [SerializeField] Animator animator;
    [SerializeField] float stunDuration;
    float stunCountdown;
    bool dead;

    [SerializeField] ParticleSystem Trail;
    [SerializeField] ParticleSystem Hit;

    bool jumping;

    public bool Grounded(bool jump)
    {
        int layermask = 1 << 8;
        if (jump && Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-3f, -groundDistance * 3f, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-3f, -groundDistance * 3f, 0), Color.green, 1);
            return true;
        }
        else if (jump && Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance * 3f, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-3f, -groundDistance * 3f, 0), Color.green, 1);
            return true;
        }
        else if (!jump && Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), groundDistance, layermask))
        {
            Debug.DrawRay(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(0, -groundDistance, 0), Color.green, 1);
            return true;
        }
        else if (!jump && Physics2D.Raycast(transform.position, Quaternion.Euler(transform.rotation.eulerAngles) * new Vector3(-.5f, -groundDistance, 0), groundDistance, layermask))
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

        Vector3 vectorToTarget = new Vector3(body.velocity.x, Mathf.Clamp(body.velocity.y, -1f, 100), 0).normalized;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg;
        Quaternion q = Quaternion.AngleAxis(angle, transform.forward);
        transform.rotation = Quaternion.Slerp(transform.rotation, q, 0.1f);

        if (Mathf.Abs(transform.rotation.z) > 90)
        {
            transform.eulerAngles = Vector3.zero;
        }

        if (body.velocity.x > maxSpeed)
        {
            body.velocity = Vector2.ClampMagnitude(body.velocity, maxSpeed);
        }

        stunCountdown -= Time.deltaTime;
        if (stunCountdown <= 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (Grounded(true))
                {
                    body.velocity = new Vector2(body.velocity.x / 3, 0);
                    body.AddForce(new Vector2(350, 800));
                    body.AddTorque(45f);

                    animator.SetTrigger("Jump");
                    animator.SetBool("Squat", false);
                    GetComponent<playerAudio>().playJump();
                    jumping = true;

                    Trail.Stop();
                }
            }

            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            {
                if (Grounded(false))
                {
                    body.AddForce(new Vector2(1, 0) * Time.deltaTime * 1000f);
                    animator.SetBool("Squat", true);
                }
                else
                {
                    body.AddForce(new Vector2(0, -1) * Time.deltaTime * 3000f);
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            animator.SetBool("Squat", false);
        }

        animator.SetBool("Jumping", jumping);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            jumping = false;
            if (Mathf.Abs(collision.relativeVelocity.y) > 10f)
            {
                animator.SetTrigger("Land");
                Trail.Play();
            }
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

            collision.transform.eulerAngles = new Vector3(0, 0, -45f);
            collision.transform.Translate(new Vector3(0, -1, 1));
            collision.enabled = false;

            GetComponent<playerAudio>().playHit();
            Hit.Stop();
            Hit.Play();
        }
        if (collision.tag == "Finish")
        {
            body.velocity = Vector2.zero;
            stunCountdown = stunDuration;
            dead = true;
            body.simulated = false;
            this.enabled = false;
        }
    }
}
