using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MaskDataUpdater : MonoBehaviour
{
    [Header("References")]
    [SerializeField] public Transform targetTransform;
    [SerializeField] private Material[] materials;

    [Header("Parameters")]
    [SerializeField] public float YOffset;
    [SerializeField] public Vector3 boxExtents = new Vector3(6f, 6f, 6f);

    GameManager gameManager;


    private void Awake()
    {
        gameManager = GameManager.instance;
    }

    private void Start()
    {
        LoadMaterials();
        Activate();
    }

    private void Update()
    {
        if (targetTransform != null)
            UpdateData();
    }

    private void OnDisable()
    {
        ResetData();
    }

    public void GetPlayerCharacter(Transform playerCharacterTransform)
    {
        targetTransform = gameManager.playerCharacter.transform;
    }

    private void LoadMaterials()
    {
        materials = Resources.LoadAll("Mask materials", typeof(Material)).Cast<Material>().ToArray();
    }

    private void UpdateData()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_Box_position", new Vector3(targetTransform.position.x, targetTransform.position.y + YOffset, targetTransform.position.z));
            materials[i].SetFloat("_YOffset", YOffset);
            materials[i].SetVector("_Box_extents", boxExtents);
        }
    }

    private void ResetData()
    {
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetVector("_Box_position", Vector3.zero);
        }
        Deactivate();
    }

    public void Activate()
    {
        LoadMaterials();
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetInt("_Activate", 1);
        }
    }

    public void Deactivate()
    {
        LoadMaterials();
        for (int i = 0; i < materials.Length; i++)
        {
            materials[i].SetInt("_Activate", 0);
        }
    }
}
