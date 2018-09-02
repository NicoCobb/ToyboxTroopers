using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//p to shoot the bullet it dies after hitting anything for debug

public class Debug_TestBullet : MonoBehaviour {
    
	// Use this for initialization
	void Start ()
    {

    }
	
	// Update is called once per frame
	void Update () {
		
        if(Input.GetKeyDown(KeyCode.P) && gameObject.GetComponent<Rigidbody>().velocity==Vector3.zero)
        {
            gameObject.GetComponent<Rigidbody>().AddForce(transform.forward * 7500);
        }
        if (Input.GetKeyDown(KeyCode.O) && gameObject.GetComponent<Rigidbody>().velocity == Vector3.zero)
        {
            gameObject.SetActive(true);
            gameObject.GetComponent<Rigidbody>().velocity = Vector3.zero;
        }

    }

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
    }
}
