using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float speed = 1.5f;
    public float horizontalLimit = 3f;
    public float verticalSpeed = 1f;

    public GameObject bulletPrefab;
    public float bulletSpeed = 2f;

    private bool fired = false;

    public float fireRate;
    private float nextFire;

    //Delegate variables to handle fuel
    public delegate void FuelHandler();
    public event FuelHandler OnFuel;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {

        //Move the player
        GetComponent<Rigidbody2D>().velocity = new Vector2(Input.GetAxis("Horizontal") * speed, verticalSpeed);

        //Keep player within bounds
        if(transform.position.x > horizontalLimit)
        {
            transform.position = new Vector2(horizontalLimit, transform.position.y);
        }
        else if(transform.position.x < -horizontalLimit)
        {
            transform.position = new Vector2(-horizontalLimit, transform.position.y);
        }



        //If fire button is pressed
        if(Input.GetAxis("Fire1") == 1f && Time.time > nextFire)
        {
            if(fired == false)
            {
                nextFire = Time.time + fireRate;

                fired = true;

                //Spawn a bullet on screen
                GameObject bulletInstance = Instantiate(bulletPrefab);

                //Spawn bullet in same place as player object
                bulletInstance.transform.SetParent(transform.parent);
                bulletInstance.transform.position = transform.position;

                //Set the bullet speed
                bulletInstance.GetComponent<Rigidbody2D>().velocity = new Vector2(0, bulletSpeed);

                //Destroy bullet after 3 seconds
                Destroy(bulletInstance, 3f);
            }
        }
        else
        {
            fired = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D otherCollider)
    {
        if (otherCollider.tag == "EnemyBullet" || otherCollider.tag == "Enemy")
        {
            Destroy(gameObject);
            Destroy(otherCollider.gameObject);
        }
        if (otherCollider.tag == "Fuel")
        {
            Destroy(otherCollider.gameObject);
            if(OnFuel != null)
            {
                OnFuel();
            }
        }
    }
}
