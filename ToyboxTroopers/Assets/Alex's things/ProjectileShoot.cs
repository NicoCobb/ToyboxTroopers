using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{

    //Projectile details
    //void Start()
    //{ 
        public Rigidbody projectile;
        public Transform bulletSpawn;
        public float force = 500f;
        public float cooldown = .25f;
        private float canFire;
    //}

	// Update is called once per frame
	void Update ()
    {
		if (Input.GetButtonDown("Fire1") && Time.time > canFire)
        {
            Rigidbody cloneRb = Instantiate(projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
            cloneRb.AddForce(bulletSpawn.transform.forward * force);
            canFire = Time.time + cooldown;
        }
	}
}
