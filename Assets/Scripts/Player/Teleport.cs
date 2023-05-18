using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        Col.transform.position = new Vector3(284.3f, -89.7f, 87.9f); // Replace the x, y, z values with your desired coordinates
        // Overrides the position of the character controller
        Physics.SyncTransforms();
    }
}
