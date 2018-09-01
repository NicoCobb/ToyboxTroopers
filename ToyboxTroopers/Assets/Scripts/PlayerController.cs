using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public Camera playerCam;

    private Rigidbody rb;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //FixedUpdate is called before physics calculations
    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        movementManager(moveHorizontal, moveVertical);
    }

    private void movementManager(float horizontal, float vertical)
    {

        Vector3 targetMovement = new Vector3(horizontal, 0.0f, vertical);
        targetMovement = playerCam.transform.TransformDirection(targetMovement);
        rb.AddForce(targetMovement * speed);

        //messing around with using torque instead since it's a ball
        //i think it can potentially feel better this way but requires much more tuning
        //Vector3 targetTorque = new Vector3(vertical, 0.0f, -horizontal);
        //targetTorque = playerCam.transform.TransformDirection(targetTorque);
        //rb.AddTorque(targetTorque * speed);
    }
}
