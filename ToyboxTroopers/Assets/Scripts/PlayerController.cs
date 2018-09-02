using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

    public float speed = 5;
    public Camera playerCam;
    public PlayerInfo data;

    private Rigidbody rb;
    private float groundCheckLength = 1f;
    private float jumpPower = 2f;
    [SerializeField]
    private float maxAngularVel = 30f;
    [SerializeField]
    private InputManager.ControllerType controllerType;
    [SerializeField]
    private int controllerNumber = 0;
    InputManager inputManager;
    // Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody>();
        rb.maxAngularVelocity = maxAngularVel;
        inputManager = new InputManager(controllerNumber, controllerType);
        if (GameSettings.instance != null)
        {
            if (data.playerNum == 1)
            {
                inputManager = GameSettings.instance.p1InputManager;
            } else
            {
                inputManager = GameSettings.instance.p2InputManager;
            }
        }

        Cursor.lockState = CursorLockMode.Locked; // sticking this here because sorry world...
        Cursor.visible = false;
	}
	
	// Update is called once per frame
	void Update () {
        inputManager.Update();
        if (inputManager.GetAxisDown(InputManager.ControllerAxis.Back))
        {
            SceneManager.LoadScene("Menu");
        }
	}

    //FixedUpdate is called before physics calculations
    private void FixedUpdate()
    {
        float moveHorizontal = inputManager.GetAxis(InputManager.ControllerAxis.HorizontalMovement);
        float moveVertical = inputManager.GetAxis(InputManager.ControllerAxis.VerticalMovement);
        bool jump = inputManager.GetAxisPressed(InputManager.ControllerAxis.Jump);

        movementManager(moveHorizontal, moveVertical, jump);
    }

    private void movementManager(float horizontal, float vertical, bool jump)
    {
        //Vector3 targetMovement = new Vector3(horizontal, 0.0f, vertical);
        //targetMovement = playerCam.transform.TransformDirection(targetMovement);
        //rb.AddForce(targetMovement * speed);

        //messing around with using torque instead since it's a ball
        //i think it can potentially feel better this way but requires much more tuning
        Vector3 targetTorque = new Vector3(vertical, 0.0f, -horizontal);
        targetTorque = playerCam.transform.TransformDirection(targetTorque);
        rb.AddTorque(targetTorque * speed);

        //check for initial jump
        if (Physics.Raycast(transform.position, -Vector3.up, groundCheckLength) && data.hasJump && jump)
        {
            //add force in upwards.
            rb.AddForce(Vector3.up * jumpPower, ForceMode.Impulse);
            data.hasDoubleJump = true;
        }

        //checks for double jump

    }
}
