using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class UIManager : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private TransitionAnimation transitionAnimation;
    [SerializeField] private GameObject interactionMenu;
    [SerializeField] private GameObject pulse;
    private Image pulseImage;

    [Header("Parameters")]
    [SerializeField] private float pulseMaxScale = 5f;
    [SerializeField] private float pulseDuration = 2f;

    private void Awake()
    {
        pulseImage = pulse.GetComponent<Image>();
    }

    private void Start()
    {
        pulse.SetActive(false);

        transitionAnimation.gameObject.SetActive(true);
        StartCoroutine(DelayedAnim());
    }



    public void ShowInteractionMenu()
    {
        interactionMenu.SetActive(true);
    }
    public void HideInteractionMenu()
    {
        interactionMenu.SetActive(false);
    }

    public void PulseAnimation(Vector3 position)
    {
        pulse.SetActive(true);
        pulse.transform.position = position;
        pulse.transform.DOScale(pulseMaxScale, pulseDuration);
        pulseImage.DOFade(0f, pulseDuration).OnComplete(ResetPulseAnimation);
    }
    private void ResetPulseAnimation()
    {
        pulseImage.color = new Color(1f, 1f, 1f, 1f);
        pulse.transform.localScale = Vector3.one;
        pulse.SetActive(false);
    }


    public void TransitionIn()
    {
        transitionAnimation.gameObject.SetActive(true);
        transitionAnimation.In();
    }

    private IEnumerator DelayedAnim()
    {
        yield return new WaitForSeconds(2f);
        TransitionIn();
    }
}
