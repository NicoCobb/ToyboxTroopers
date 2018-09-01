using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_OnHit : MonoBehaviour {

    public GameObject[] broken_cubes;
    
    static private int breaking_point = 150;

    private Rigidbody rb;
	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}

    //checks when this object hits something. if it hits something and the impulse reaches over its breaking point 
    //(while also making sure the collision isn't just 2 literal pixels grazing each other on the y axis because towers)
    //it despawns the "not fractured" cube and spawned one of 4 broken ones to do physics with.
    //if you shoot the cube hard enough / another cube hits it hard enough it can break before it hits the ground/
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.sqrMagnitude / rb.mass > breaking_point && (collision.impulse.y > 0.1f))
        {
            Destroy(gameObject);
            GameObject child = Instantiate(broken_cubes[Random.Range(0, broken_cubes.Length)], transform.position, transform.rotation, gameObject.transform.parent);
            foreach (Rigidbody r in child.GetComponentsInChildren<Rigidbody>())
            {
                r.velocity = rb.velocity;

                r.angularVelocity = rb.angularVelocity;

            }

        }
    }
}
