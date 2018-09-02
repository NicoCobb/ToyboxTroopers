using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunControl : MonoBehaviour
{

    public Transform player;
    private Transform gun;
    public Vector3 offset;

    void Start()
    {
        gun = transform;
    }
	
	// Update is called once per frame
	void Update ()
    {
        gun.position = player.position + offset;
	}
}
