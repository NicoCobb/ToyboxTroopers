using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_OnHit : MonoBehaviour {

    public GameObject[] broken_cubes;
    
    static private int breaking_point = 150;
	static private float break_multi = 1.25f;

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
        if (collision.impulse.sqrMagnitude / rb.mass > breaking_point || collision.gameObject.tag == "bullet")
        {
            Vector3 angle = rb.velocity;
            if (collision.gameObject.tag == "bullet")
            {
                angle = collision.contacts[0].normal;
                angle /= 2;
                print(rb.velocity + " " + collision.contacts[0].normal + " " + angle);
                
            }
            else
                angle = Vector3.one;
            angle = -Vector3.Normalize(angle);

            print(angle);

            Destroy(gameObject);
            GameObject child = Instantiate(broken_cubes[Random.Range(0, broken_cubes.Length)], transform.position, transform.rotation, gameObject.transform.parent);
            foreach (Rigidbody r in child.GetComponentsInChildren<Rigidbody>())
            {
				//Net force of the total box
				Vector3 force = rb.mass * rb.velocity;

                for (int i = 0; i < 3; i++)
                    force[i] = Mathf.Abs(force[i]);


                r.velocity = Vector3.Scale(force / r.mass, angle);
				r.velocity *= break_multi;
				
                r.angularVelocity = rb.angularVelocity;

            }
        }
    }
}
