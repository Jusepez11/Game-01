using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public Text txt;
    public float speed = 110f;
    public Rigidbody2D rb;
    public int damage = 10;
    
    // Start is called before the first frame update
    void Start()
    {
        rb.velocity = transform.right * speed;
    }

    private void Update()
    {
        //Debug.Log(rb.velocity);
        float y = rb.velocity.y;
        float x = rb.velocity.x;
        if (Mathf.Sqrt(Mathf.Pow(x, 2) + Mathf.Pow(y, 2)) < (speed-5f) || (transform.position.x < -1000 || transform.position.x > 0 || transform.position.y < -500 || transform.position.y > 500))
        {
            Destroy(gameObject);
        }

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        Debug.Log(hitInfo.name);
        PlayerController player = hitInfo.GetComponent<PlayerController>();
        if (player != null)
        {
            player.Die(); // TakeDamage(damage);
        }
        
        if (hitInfo.tag != "weapon")
        {
            Destroy(gameObject);
        }
            
    }

}
