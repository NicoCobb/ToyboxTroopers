using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeOnImpact : MonoBehaviour
{

    public float deleteThis = .300f;
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 9)
        {
            //Explode
            Destroy(transform.gameObject);
        }
        else if (collision.gameObject.layer == 10)
        {
            //Ricochet

        }
    }

    void Update()
    {
        deleteThis--;
        if (deleteThis <= 0)
            Destroy(transform.gameObject);
    }
}
