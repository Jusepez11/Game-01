using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunPicking : MonoBehaviour
{
    public float distance = 10f;
    public GameObject equipPosition;
    public GameObject selectedWeapon;
    public GameObject equipedWeapon;
    public GameObject player;
    public bool canGrab;
    public bool picked = false;


    private void Update()
    {
        if (picked && equipedWeapon != null)
        {
            equipedWeapon.transform.position = equipPosition.transform.position;
            equipedWeapon.transform.rotation = equipPosition.transform.rotation;
        }

        if (equipedWeapon == null)
        {
            picked = false;
        }

        if (canGrab && equipedWeapon == null && picked == false)
        {
            if (Input.GetMouseButton(1)) {
                Debug.Log("Grabbing");
                PickUp(); 
            }
            picked = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //If player touches gun it can be grab
        if (collision.transform.tag == "weapon")
        {
            Debug.Log("I can grab it!");
            selectedWeapon = collision.transform.gameObject;
            canGrab = true;

        }
        else{
            canGrab = false;
        }
    }


    private void PickUp()
    {


        equipedWeapon = selectedWeapon;
        selectedWeapon.GetComponent<BoxCollider2D>().enabled = false;
        selectedWeapon.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        selectedWeapon.transform.position = equipPosition.transform.position;
        selectedWeapon.transform.parent = equipPosition.transform;
        selectedWeapon.transform.localEulerAngles = new Vector3(0f, 0f, 0f);

        selectedWeapon.GetComponent<Shootable>().possible = true;
        player.GetComponent<PlayerController>().gunSprite = selectedWeapon.GetComponent<SpriteRenderer>();
        player.GetComponent<PlayerController>().firepoint = selectedWeapon.transform.GetChild(0);
        //selectedWeapon.GetComponent<Rigidbody>().isKinematic = true;

        Debug.Log("Picked it up");
    }
}