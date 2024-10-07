using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Uzi : MonoBehaviour
{
    public Transform firepoint;
    public float fireRate;
    public GameObject bulletPrefab;
    public GameObject arm;

    void Start()
    {
        Bullet bllt = GetComponent<Bullet>();
        bllt.speed = fireRate;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
    }
}
