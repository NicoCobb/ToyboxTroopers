using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunRaycast : MonoBehaviour {

    public GameObject gunContainer;
    [SerializeField]
    private InputManager.ControllerType controllerType;
    [SerializeField]
    private int controllerNumber = 0;
    InputManager inputManager;

    // Use this for initialization
    void Start () {
        inputManager = new InputManager(controllerNumber, controllerType);
    }
	
	// Update is called once per frame
	void Update () {
        inputManager.Update();
    }

    private void FixedUpdate()
    {
        bool fire = inputManager.GetAxisPressed(InputManager.ControllerAxis.Shoot);
        RaycastHit hit;
        if (fire && Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, Mathf.Infinity))
        {
            Transform focalPoint = hit.transform;
            gunContainer.transform.LookAt(focalPoint);
        }
    }
}
