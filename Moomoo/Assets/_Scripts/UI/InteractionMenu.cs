using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class InteractionMenu : MonoBehaviour
{
    [SerializeField] private RectTransform rectTransform;
    // [SerializeField] private RectTransform interactionFrame;
    
    [Header("Move")]
    private Vector3 originalPosition;
    [SerializeField] private Vector3 newPosition;
    [SerializeField] private float moveDuration;
    
    //[Header("Scale")]
    //private Vector2 originalScale;
    //[SerializeField] private Vector2 newScale;
    //[SerializeField] private float scaleDuration;

    private void Start()
    {
        originalPosition = rectTransform.localPosition;
        //originalScale = rectTransform.localScale;
    }

    private void OnEnable()
    {
        transform.DOLocalMove(newPosition, moveDuration).SetEase(Ease.OutCubic);
        // interactionFrame.transform.DOScale(newScale, scaleDuration).SetEase(Ease.InOutCubic);
    }

    private void OnDisable()
    {
        // Detener el movimiento de tweening de alguna manera
        rectTransform.localPosition = originalPosition;
        //rectTransform.localScale = originalScale;
    }
}
