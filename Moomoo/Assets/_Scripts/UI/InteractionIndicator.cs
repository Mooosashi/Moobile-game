using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UI.Extensions;
using DG.Tweening;
using TMPro;

public class InteractionIndicator : MonoBehaviour
{
    [Header("Parameters")]
    [SerializeField] private float inDuration = 0.5f;
    [SerializeField] private float outDuration = 0.1f;

    [Header("References")]
    [SerializeField] private UILineRenderer lineRenderer;
    [SerializeField] private RectTransform textFrameTransform;
    [SerializeField] private RectTransform indicatorTransform;
    [SerializeField] private TextMeshProUGUI text;

    private GameManager gameManager;
    private Camera mainCamera;
    private bool activate;
    private Transform target;
    private string interactableName;
    private Image textFrameImage;

    private Collider targetCollider;


    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.onInRangeOfInteractable += Activate;
        gameManager.onOutRangeOfInteractable += Deactivate;
    }

    private void Start()
    {
        mainCamera = Camera.main;
        
        textFrameImage = textFrameTransform.GetComponent<Image>();

        indicatorTransform.localScale = new Vector2(0f, 0f);
        lineRenderer.color = new Vector4(255, 255, 255, 0);
        textFrameImage.color = new Vector4(255, 255, 255, 0);

        lineRenderer.Points[1] = textFrameTransform.localPosition;
    }

    private void Update()
    {
        if (activate)
        {
            indicatorTransform.position = mainCamera.WorldToScreenPoint(targetCollider.bounds.center);

            lineRenderer.Points[0] = indicatorTransform.localPosition;
            lineRenderer.SetAllDirty();
        }
    }

    private void Activate(GameObject interactable)
    {
        activate = true;

        target = interactable.transform;
        targetCollider = interactable.GetComponent<Collider>();

        interactableName = interactable.GetComponent<Interactable>().interactableName;
        text.text = interactableName;

        indicatorTransform.DOScale(1f, inDuration);

        StartCoroutine(LineRendererFade(1f, inDuration));

        textFrameImage.DOFade(1f, inDuration);
    }

    private void Deactivate()
    {
        target = null;
        
        lineRenderer.DOFade(0f, outDuration);

        indicatorTransform.DOScale(0f, outDuration);

        textFrameImage.DOFade(0f, outDuration).OnComplete(() =>
        {
            text.text = "";
        });

        activate = false;
    }

    IEnumerator LineRendererFade(float endValue, float duration)
    {
        yield return new WaitForSeconds(0.1f);
        lineRenderer.DOFade(endValue, duration);
    }


    private void OnDestroy()
    {
        gameManager.onInRangeOfInteractable -= Activate;
        gameManager.onOutRangeOfInteractable -= Deactivate;
    }
}
