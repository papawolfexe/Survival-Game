using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DungeonIn  : MonoBehaviour
{
    void OnTriggerEnter(Collider Col)
    {
        Col.transform.position = new Vector3(952.065f, -41.43115f, 325.927f); // Replace the x, y, z values with your desired coordinates
        // Overrides the position of the character controller
        Physics.SyncTransforms();
    }
}
