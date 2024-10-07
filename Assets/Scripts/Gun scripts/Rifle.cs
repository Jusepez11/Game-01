using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Rifle : MonoBehaviour
{
    public Transform firepoint;
    public GameObject bulletPrefab;
    public bool shootable;
    public float fireRate;
    public float bulletSpeed = 150f;
    public float cooldownSeconds = 5;
    public float cooldown;
    public int maxAmmo = 30;
    public int ammo;
    private float nextFire;

    public GameObject arm;


    [Header("Bullet HUD")]
    public Sprite ammoSprite;
    public Image bulletHUD;
    public Text ammoLeft;

    private void Start()
    {
        ammo = maxAmmo;
    
    }

    void OnTriggerEnter2D(Collider2D hitInfo)
    {
        if (arm == null) return;
        if (arm.GetComponent<Throwing_Gun>().throwed)
        {
            Debug.Log(hitInfo.name);
            PlayerController player = hitInfo.GetComponent<PlayerController>();
            if (player != null)
            {
                player.Die(); // TakeDamage(damage);
            }
            /**
            if (hitInfo.tag != "weapon")
            {
                Destroy(gameObject);
            }
                **/  
        }
    }


    void Update()
    {
        arm = transform.parent.gameObject == null ? null : transform.parent.gameObject;
        

        if (shootable)
        {
            bulletHUD.sprite = ammoSprite;
            bulletHUD.preserveAspect = true;
            bulletHUD.color = new Color(255f, 255f, 255f, 255f);
            ammoLeft.text = ammo.ToString();
        }

        shootable = GetComponent<Shootable>().possible;
        if (fireRate == 0 && Input.GetButtonDown("Fire1") && shootable)
        {   //Reduced to a single if, cause It does exactly the same
            //And in my Opinion, looks better. (You might want not to
            //in case you have anything else here that do needs the if)
            Shoot();
        }
        else
        {
            if (Input.GetButton("Fire1") && Time.time > nextFire && fireRate > 0 && shootable)
            {
                //I added "&& fireRate > 0", because if not, this will run if the user decides
                //to hold the button, as "GetButtonDown" only returns true the frame the button
                //is pressed, and while its hold, is false, so the "else" will run, and so will this.
                if (ammo > 0)
                {   //If you have ammo
                    nextFire = Time.time + fireRate;
                    Shoot();
                    --ammo; //Explained by itself
                    ammoLeft.text = ammo.ToString();
                }
                if (ammo == 0)
                {   //If you no longer have ammo
                    if (cooldown > Time.time)
                    {   //If there is no cooldown (relatively)
                        cooldown = Time.time + cooldownSeconds;
                        ammoLeft.text = ammo.ToString();
                    }
                }
            }
        }
    }

    void Shoot()
    {
        Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
        Debug.Log(firepoint.position);
    }


}
