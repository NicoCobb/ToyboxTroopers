using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cube_OnHit : MonoBehaviour {

    public GameObject[] broken_cubes;
    
    static private int breaking_point = 125;
	static private float break_multi = 1.25f;
    static private float spawn_addition = 35f;

    //How long until the building fades away.
    public static float FadeTime = 3.006125f;
    //How long untill the buildings respawn; this time is how long to wait after the cubes BEGIN to fade.
    public static float RespawnTime = 4.0f;

    //The building that this cube is part of; this is set in BuldingRespawn.cs 
    public BuildingRespawn building;

    private Rigidbody rb;
    //This is used for teleporting the cube.
    private Vector3 initPos;
    private Quaternion initRot;

    private bool breakable = true;
	
	void Start () {
        rb = GetComponent<Rigidbody>();
        initPos = gameObject.transform.position;
        initRot = rb.rotation;
    }

    public void DisableColliders()
    {
        //Disable the box collider on the cube.
        GetComponent<BoxCollider>().enabled = false;
    }

    private IEnumerator WaitForReset()
    {
        yield return new WaitForSeconds(RespawnTime);
        ResetCube();
    }

    public void FadeAway()
    {
        rb.velocity = Vector3.zero;
        StartCoroutine("Fade", 0.0f);
        StartCoroutine("WaitForReset") ;
    }

    private IEnumerator Fade(float endTrans)
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / FadeTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, endTrans, t));
            GetComponent<Renderer>().material.color = newColor;
       
            yield return null;
        }
    }

    public void ResetCube()
    {
        gameObject.GetComponent<MeshRenderer>().enabled = true;
        transform.position = new Vector3(initPos.x, initPos.y, initPos.z);
        rb.velocity = Vector3.zero;
        rb.useGravity = false;
        rb.angularVelocity = Vector3.zero;
        rb.rotation = initRot;
        StartCoroutine("Fade", 1.0f);
        StartCoroutine("FinishReset");
    }

    private IEnumerator FinishReset()
    {
        yield return new WaitForSeconds(FadeTime);
        GetComponent<BoxCollider>().enabled = true;
        rb.useGravity = true;

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.impulse.sqrMagnitude / rb.mass > breaking_point || collision.gameObject.tag == "bullet")
        {
            //Disable the cube's mesh and box collider
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            gameObject.GetComponent<BoxCollider>().enabled = false;
            //gameObject.SetActive(false);

            if (building)
                building.Hit();

            //Get the angle it retursn from
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


            GameObject child = Instantiate(broken_cubes[Random.Range(0, broken_cubes.Length)], transform.position, transform.rotation, gameObject.transform.parent);
            foreach (Rigidbody r in child.GetComponentsInChildren<Rigidbody>())
            {
				//Use base math to transfer momentums.
				Vector3 momentum = rb.mass * rb.velocity;

                for (int i = 0; i < 3; i++)
                    momentum[i] = Mathf.Abs(momentum[i]);


                r.velocity = Vector3.Scale(momentum / r.mass, angle);
				r.velocity *= break_multi;
				
                r.angularVelocity = rb.angularVelocity;

            }
        }
    }
}
