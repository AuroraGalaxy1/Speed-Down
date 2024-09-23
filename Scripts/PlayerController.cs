using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;
    public float speed;
    public float checkRadius;
    public LayerMask platform;
    public GameObject groundCheck;
    float xVelocity;
    public bool isOnGround;
    bool playerdead;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        isOnGround = Physics2D.OverlapCircle(groundCheck.transform.position, checkRadius, platform);
        Movement();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Spike"))
        {
            anim.SetTrigger("dead");
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Fan"))
        {
            rb.velocity = new Vector2(rb.velocity.x, 10f);
        }
    }


    public void Playerdead()
    {
        playerdead = true;
        GameManager.GameOver(playerdead);
    }
    void Movement()
    {
        xVelocity = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(xVelocity * speed, rb.velocity.y);

        anim.SetFloat("speed", Mathf.Abs(rb.velocity.x));
        if (xVelocity != 0)
        {
            transform.localScale = new Vector3(xVelocity, 1, 1);
        }
    }


}
