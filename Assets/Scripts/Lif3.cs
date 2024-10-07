using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Respawn : MonoBehaviour
{

    //Health
    public float health = 100f;

    //Respawing
    private Rigidbody2D rb;
    private Vector3 RespawnPoint;
    public Text deadTimer;
    public float fallLimit;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        RespawnPoint = transform.position;
    }

    void FixedUpdate()
    {
        if (transform.position.y < fallLimit) 
        { 
            transform.position = RespawnPoint;
            rb.velocity *= 0;
        }
    }
    public void takeDamage(int damage)
    {
        health -= damage;

        if (health <= 0 || transform.position.y < fallLimit)
        {
            die();
        }
    }

    void die()
    {
        transform.position = RespawnPoint;
        rb.velocity *= 0;
    }



}