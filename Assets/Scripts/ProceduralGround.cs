using System;
using UnityEngine;

public class ProceduralGround : MonoBehaviour
{
    public GameObject GeneratedObjectHolder;
    public Material PrimaryMaterial;
    public Material SecondaryMaterial;

    public int StartXLocation = -46;
    public int StartZLocation = 48;

    public int CountX = 20;
    public int CountZ = 20;

    public float CubeSize = 2.0f;
    public float CubeSpacingMultiplier = 0.01f;

    public void Start()
    {
        var cubeSpacing = CubeSize + CubeSize * CubeSpacingMultiplier;
        var random = new System.Random();
        int layerGround = LayerMask.NameToLayer("Ground");

        for (var x = 0; x < CountX; x++)
        {
            for (var z = 0; z < CountZ; z++)
            {
                var position = new Vector3(
                    StartXLocation - x * cubeSpacing,
                    1f,
                    StartZLocation + z * cubeSpacing
                );
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                cube.transform.position = position;
                cube.layer = layerGround;
                cube.transform.localScale = new Vector3(CubeSize, CubeSize, CubeSize);
                cube.transform.parent = GeneratedObjectHolder.transform;

                var cubeRenderer = cube.GetComponent<Renderer>();
                if (random.NextDouble() < 0.1)
                {
                    cubeRenderer.material = SecondaryMaterial;
                }
                else
                {
                    cubeRenderer.material = PrimaryMaterial;
                }
            }
        }
    }
}
