using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{

    public Transform player;
    public Transform playerCam;
    public Transform focalPoint;
    public Vector3 offset;

    void Start()
    {
    }
	
	// Update is called once per frame
	void Update ()
    {
        transform.position = player.position + offset;
        //transform.LookAt(focalPoint);
    }
}
