using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBullets : MonoBehaviour
{

	void OnCollisionEnter( Collision collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            Physics.IgnoreCollision(collision.gameObject.GetComponent<Collider>(), GetComponent<Collider>());
        }
    }
}
