using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionMenu : MonoBehaviour
{
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float duration;

    private void Start()
    {
        originalPosition = transform.position;
        
    }

    private void OnEnable()
    {
        transform.DOMove(newPosition, duration);
    }

    private void OnDisable()
    {
        // Detener el movimiento de tweening de alguna manera
        transform.position = originalPosition;
    }
}
