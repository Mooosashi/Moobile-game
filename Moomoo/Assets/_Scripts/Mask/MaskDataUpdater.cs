using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskDataUpdater : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;

    [Header("Materials")]
    public Material bigCube;
    public Material terrain;
    public Material boxCase;
    public Material boxInside;
    public Material grass;

    private void Start()
    {
        Enable();
    }

    private void Update()
    {
        UpdateData();
    }

    private void OnDisable()
    {
        ResetData();
    }

    private void UpdateData()
    {
        bigCube.SetVector("_Box_position", targetTransform.position);
        terrain.SetVector("_Box_position", targetTransform.position);
        boxCase.SetVector("_Box_position", targetTransform.position);
        boxInside.SetVector("_Box_position", targetTransform.position);
        grass.SetVector("_Box_position", targetTransform.position);
    }

    private void ResetData()
    {
        bigCube.SetVector("_Box_position", Vector3.zero);
        terrain.SetVector("_Box_position", Vector3.zero);
        boxCase.SetVector("_Box_position", Vector3.zero);
        boxInside.SetVector("_Box_position", Vector3.zero);
        grass.SetVector("_Box_position", Vector3.zero);
    }

    public void Enable()
    {
        bigCube.SetInt("_Activate", 1);
        terrain.SetInt("_Activate", 1);
        boxCase.SetInt("_Activate", 1);
        boxInside.SetInt("_Activate", 1);
        grass.SetInt("_Activate", 1);
    }

    public void Disable()
    {
        bigCube.SetInt("_Activate", 0);
        terrain.SetInt("_Activate", 0);
        boxCase.SetInt("_Activate", 0);
        boxInside.SetInt("_Activate", 0);
        grass.SetInt("_Activate", 0);
    }
}
