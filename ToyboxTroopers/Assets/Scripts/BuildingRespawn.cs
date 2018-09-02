using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingRespawn : MonoBehaviour {

    //How long does the building last for after half of the objects are hit.
    public static float LifeTime = 4.0125f;
    //How long until the building fades away.
    public static float FadeTime = 3.006125f;


    private List<Cube_OnHit> cube_list;
    private int cubes_hit; 
    
	// Use this for initialization
	void Start () {
        cube_list = new List<Cube_OnHit>();

        foreach (Cube_OnHit c in GetComponentsInChildren<Cube_OnHit>())
        {
            cube_list.Add(c);
            c.building = this;
        }
	}

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
            ResetBuilding();
    }
    public void Hit()
    {
        cubes_hit += 1;
        print(cubes_hit + " " + cube_list.Count / 2);
        if(cubes_hit > cube_list.Count / 2)
        {
            print("Resetting the building in " + LifeTime + " seconds.");
            ResetBuilding();
        }
    }

    private void ResetBuilding()
    {
        //If we call this while the Wait funciton is still going on, we shouldnot 
        StopCoroutine("Wait");
        StartCoroutine("Wait");
    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(LifeTime);
        FadeBuilding();
    }

    private void FadeBuilding()
    {
        foreach (Cube_OnHit c in cube_list)
        {
            c.DisableColliders();
            c.FadeAway();
            
        }
        cubes_hit = 0;

    }

}
