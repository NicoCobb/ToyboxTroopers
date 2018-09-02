using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetSquished : MonoBehaviour
{
    public PlayerInfo data;
	
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 8)
        {
            //Pushback from bullets
        }
        else if (collision.gameObject.layer == 9)
        {
            //Get squished
            //Need a formula for calculating damage done
            int damage = (int) Mathf.Ceil((collision.relativeVelocity.magnitude)*collision.rigidbody.mass/5.0f);
            Debug.Log(damage);

            Debug.Log(transform.localScale);
            data.HP -= damage;
            //Debug.Log(data.HP);
            float reduction = (float) ((float)data.HP / 100.0f);
            //Debug.Log(reduction);
            transform.localScale *= reduction;
            //Debug.Log(transform.localScale);     
        }
    }
}
