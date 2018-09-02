using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DespawnFragments : MonoBehaviour {

    //How long do fragments last for after they hit an object
    public static float LifeTime = 1.0125f;
    //How long until they fade away.
    public static float FadeTime = 3.006125f;
    private bool isFading = false;

    private void OnCollisionEnter(Collision collision)
    {
        if(!isFading)
        { 
            StartCoroutine("Wait");
            isFading = true;
        }
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(LifeTime);
        //When they begin to fade, disable their collidesr.
        GetComponent<MeshCollider>().enabled = false;
        StartCoroutine("Fade");
    }

    private IEnumerator Fade()
    {
        float alpha = GetComponent<Renderer>().material.color.a;
        for (float t = 0.0f; t < 1.0f; t += Time.deltaTime / FadeTime)
        {
            Color newColor = new Color(1, 1, 1, Mathf.Lerp(alpha, 0.0f, t));
            GetComponent<Renderer>().material.color = newColor;
            if (newColor.a <= 0.05)
            {
                Destroy(gameObject);
            }
            yield return null;
        }
    }

}
