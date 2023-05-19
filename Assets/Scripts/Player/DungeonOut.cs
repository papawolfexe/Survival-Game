using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonOut  : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        Col.transform.position = new Vector3(900.55f, 27.25f, 291.81f); // Replace the x, y, z values with your desired coordinates
        // Overrides the position of the character controller
        Physics.SyncTransforms();
    }
}
