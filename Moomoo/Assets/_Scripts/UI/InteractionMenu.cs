using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    [SerializeField] private Vector3 originalPosition;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float duration;

    private void Start()
    {
        originalPosition = rectTransform.localPosition;
    }

    private void OnEnable()
    {
        transform.DOLocalMove(newPosition, duration);
    }

    private void OnDisable()
    {
        // Detener el movimiento de tweening de alguna manera
        rectTransform.localPosition = originalPosition;
    }
}
