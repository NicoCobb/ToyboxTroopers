using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreBullets : MonoBehaviour
{

    void Start()
    {
        Physics.IgnoreLayerCollision(8, 8);
    }
}
