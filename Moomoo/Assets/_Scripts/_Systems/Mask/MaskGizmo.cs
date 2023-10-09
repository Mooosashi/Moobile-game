using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaskGizmo : MonoBehaviour
{
    [SerializeField] private MaskDataUpdater maskDataUpdater;

    [Header("Parameters")]
    [SerializeField] Color color = Color.white;
    [SerializeField] Vector3 target;
    [SerializeField] Vector3 scale;


    private void OnDrawGizmos()
    {
        Vector3 targetPosition = maskDataUpdater.targetTransform.position;
        target = new Vector3(targetPosition.x, targetPosition.y + maskDataUpdater.YOffset, targetPosition.z);
        scale = maskDataUpdater.boxExtents * 2;
        Gizmos.color = color;
        Gizmos.DrawCube(target, scale);
    }
}
