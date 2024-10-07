using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Pistol : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public bool shootable;
    public float fireRate=0;
    public int maxAmmo = 30;
    public int ammo;

    [Header("Bullet HUD")]
    public Sprite ammoSprite;
    public Image bulletHUD;
    public Text ammoLeft;

    private void Start()
    {
        ammo = maxAmmo;
    }

    void Update()
    {
        if (shootable)
        {
            bulletHUD.sprite = ammoSprite;
            bulletHUD.preserveAspect = true;
            bulletHUD.color = new Color(255f, 255f, 255f, 255f);
            ammoLeft.text = ammo.ToString();
        }

        shootable = GetComponent<Shootable>().possible;
        if (fireRate == 0 && Input.GetButtonDown("Fire1") && shootable && ammo > 0)
        {   //Reduced to a single if, cause It does exactly the same
            //And in my Opinion, looks better. (You might want not to
            //in case you have anything else here that do needs the if)
            Shoot();
            --ammo;
            ammoLeft.text = ammo.ToString();
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Debug.Log(firepoint.position);
    }


}
