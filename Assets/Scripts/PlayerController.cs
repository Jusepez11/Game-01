using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //[Header("")]
    /*
    [Header("Health")]
    public Text life;
    private float iHealth;
    public float health = 100f;
    */

    [Header("Basic movement stuff")]
    public float speed;
    public float jumpForce = 50;
    private float moveInput;
    private Rigidbody2D rb;

    [Header("Flipping")]
    public bool facingRight = true;
    public SpriteRenderer gunSprite; //gun flipping
    public Transform firepoint;

    [Header("Ground")]
    public bool isGrounded;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatIsGround;

    [Header("Multiple Jumps")]
    private int jumps;
    public int extraJumps;

    [Header("Respawning")]
    private Vector3 RespawnPoint;
    public Text deadTimer;
    public float fallLimit;

    void Start()
    {
        jumps = extraJumps;
        //iHealth = health;
        RespawnPoint = transform.position;
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        if (transform.position.y < fallLimit){
            Die();
        }

        //life.text = "Health: " + health.ToString();

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatIsGround);

        moveInput = Input.GetAxisRaw("Horizontal");
        rb.velocity = new Vector2(moveInput * speed, rb.velocity.y);

        //Changing the direction the gun is facing
        if (facingRight == false && moveInput > 0)
        {
            Flip();
            if (gunSprite != null)
            {
                gunSprite.flipY = false;
                //firepoint.position = new Vector3(6.937f, -0.37f, 0f);
            }
        }
        else if (facingRight == true && moveInput < 0)
        {
            Flip(); 
            if (gunSprite != null)
            {
                gunSprite.flipY = true; 
                //firepoint.position = new Vector3(6.937f, 0.37f, 0f);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {

        //Jumps
        if (isGrounded)
        {
            jumps = extraJumps;
        }

        if (Input.GetButton("Jump") && jumps > 0)
        {
            rb.velocity += Vector2.up * jumpForce;
            --jumps;
        }
        else if (Input.GetButton("Jump") && (isGrounded))
        {
            rb.velocity += Vector2.up * jumpForce;
        }
        //Debug.Log(rb.velocity);

    }
    /*
    public void TakeDamage(int damage)
    {
        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }
    */

    //Changing the direction the player facing
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void Die()
    {
        transform.position = RespawnPoint;
        rb.velocity *= 0;
        //health = iHealth;
    }
}
