using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{

    public float deleteThis = 500;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //Explode
            Destroy(transform.gameObject);
        }
        else if (collision.gameObject.layer == 10 || collision.gameObject.layer == 11)
        {
            //Ricochet
            Vector3 normal = collision.contacts[0].normal;
            transform.position = Vector3.Reflect(transform.position, normal);
        }
    }

    void Update()
    {
        deleteThis--;
        if (deleteThis <= 0)
            Destroy(transform.gameObject);
    }
}
