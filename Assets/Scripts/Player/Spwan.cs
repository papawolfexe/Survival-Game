using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spwan : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        Col.transform.position = new Vector3(120.2f, 8.3f, -38.4f); // Replace the x, y, z values with your desired coordinates
        // Overrides the position of the character controller
        Physics.SyncTransforms();
    }
}
