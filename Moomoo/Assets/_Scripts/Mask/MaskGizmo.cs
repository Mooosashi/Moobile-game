using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskGizmo : MonoBehaviour
{
    [Header("Bounding box")]
    [SerializeField] Color color = Color.white;
    [SerializeField] Vector3 position;
    [SerializeField] Vector3 scale;
    [SerializeField] GameObject target;

    private void OnDrawGizmos()
    {
        Gizmos.color = color;
        if (target == null)
        {
            Gizmos.DrawCube(position, scale);
        }
        else
        {
            Gizmos.DrawCube(target.transform.position, scale);
        }
    }
}
