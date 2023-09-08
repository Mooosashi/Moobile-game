using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaskDataUpdater : MonoBehaviour
{
    [SerializeField] private Transform targetTransform;
    [SerializeField] private Material[] materials;


    private void Start()
    {
        LoadMaterials();
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

    private void LoadMaterials()
    {
        materials = Resources.LoadAll("Mask materials", typeof(Material)).Cast<Material>().ToArray();
    }

    private void UpdateData()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_Box_position", targetTransform.position);
        }
    }

    private void ResetData()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_Box_position", Vector3.zero);
        }
    }

    public void Enable()
    {
        LoadMaterials();
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetInt("_Activate", 1);
        }
    }

    public void Disable()
    {
        LoadMaterials();
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetInt("_Activate", 0);
        }
    }
}
