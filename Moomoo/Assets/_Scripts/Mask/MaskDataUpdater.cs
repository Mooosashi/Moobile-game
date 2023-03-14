using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDataUpdater : MonoBehaviour
{
    public Material bigCube;
    public Material terrain;
    public Material boxCase;
    public Material boxInside;
    public Material grass;

    private void Update()
    {
        bigCube.SetVector("_Box_position", transform.position);
        terrain.SetVector("_Box_position", transform.position);
        boxCase.SetVector("_Box_position", transform.position);
        boxInside.SetVector("_Box_position", transform.position);
        grass.SetVector("_Box_position", transform.position);
    }

    private void OnDisable()
    {
        bigCube.SetVector("_Box_position", Vector3.zero);
        terrain.SetVector("_Box_position", Vector3.zero);
        boxCase.SetVector("_Box_position", Vector3.zero);
        boxInside.SetVector("_Box_position", Vector3.zero);
        grass.SetVector("_Box_position", Vector3.zero);
    }
}
