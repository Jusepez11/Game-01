using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throwing_Gun : MonoBehaviour
{
    public float speed = 30f;
    private Rigidbody2D rb;
    private GunPicking gunPicking;
    private GameObject arm;

    public bool throwed = false;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        gunPicking = GetComponent<GunPicking>();
        arm = gunPicking.equipPosition.gameObject;
    }

    void Update()
    {
        if (gunPicking.picked && Input.GetMouseButtonDown(1))
        {
            ThrowWeapon();
            throwed = true;
        }
    }

    private void ThrowWeapon()
    {
        if (rb == null) return;

        gunPicking.equipedWeapon = null;
        Debug.Log("Da child is:"+arm.transform.GetChild(0));
        arm.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        arm.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        arm.transform.GetChild(0).gameObject.GetComponent<Shootable>().possible = false;
        arm.transform.DetachChildren();

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - transform.position).normalized;

        rb.velocity = direction * speed;
        //rb.isKinematic = false;  // Ensure the Rigidbody2D is not kinematic
       // rb.constraints = RigidbodyConstraints2D.None;  // Remove constraints if any

        Debug.Log("Throwing");
    }
}
