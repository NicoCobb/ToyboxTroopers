using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileShoot : MonoBehaviour
{

    //Projectile details
    public Rigidbody projectile;
    public Transform bulletSpawn;
    public float force = 500f;
    public float cooldown = .25f;
    public int accuracy = 15;
    private float canFire;
    AudioSource audio;

    //input manager
    [SerializeField]
    private PlayerInfo data;
    InputManager inputManager;

    private void Start()
    {
        if (GameSettings.instance != null)
        {
            Debug.Log("Trying to set camera control");
            if (data.playerNum == 1)
            {
                inputManager = GameSettings.instance.p1InputManager;
            }
            else
            {
                inputManager = GameSettings.instance.p2InputManager;
            }
        }
        audio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update ()
    {
        //inputManager.Update(); // this is done in playercontroller since they share an inputmanager
        bool fire = inputManager.GetAxisPressed(InputManager.ControllerAxis.Shoot);
        if (fire && Time.time > canFire)
        {
            audio.pitch = Random.Range(.9f, 1.5f);
            audio.Play();
            for (int x = 0; x < 3; ++x)
            {
                for (int y = 0; y < 3; ++y)
                {
                    Rigidbody cloneRb = Instantiate(projectile, bulletSpawn.position, Quaternion.identity) as Rigidbody;
                    Vector3 direction = new Vector3();
                    float rx = Random.value + x - 1;
                    float ry = Random.value + y - 1;
                    direction = new Vector3(rx / accuracy, ry / accuracy, 1);
                    cloneRb.AddForce(direction * force);
                }
            }
            canFire = Time.time + cooldown;
        }
	}
}
