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
    [SerializeField] private GameObject UIJoystick;
    [SerializeField] private Image joystickImage;
    [SerializeField] private Image joystickHandleImage;

    [Header("Parameters")]
    [SerializeField] private float pulseMaxScale = 5f;
    [SerializeField] private float pulseDuration = 2f;
    [SerializeField] private float joystickFadeDuration = 0.5f;

    private GameManager gameManager;


    private void Awake()
    {
        pulseImage = pulse.GetComponent<Image>();

        gameManager = GameManager.instance;
        gameManager.onFreeMovementState += ShowJoystick;
        gameManager.onFreeMovementState += HideInteractionMenu;

        gameManager.onInteractingState += HideJoystick;
        gameManager.onInteractingState += ShowInteractionMenu;

        gameManager.onTransitionState += TransitionIn;
    }

    private void Start()
    {
        pulse.transform.localScale = new Vector2(0f, 0f);
    }

    private void OnLevelWasLoaded()
    {
        UIJoystick.SetActive(false);
    }


    public void ShowInteractionMenu(GameObject interactable)
    {
        interactionMenu.SetActive(true);
    }
    public void HideInteractionMenu()
    {
        interactionMenu.SetActive(false);
    }

    private void ShowJoystick()
    {
        UIJoystick.SetActive(true);
        
        Vector4 noAlpha = new Vector4(joystickImage.color.r, joystickImage.color.b, joystickImage.color.g, 0f);
        joystickImage.color = noAlpha;
        joystickHandleImage.color = noAlpha;

        joystickImage.DOFade(1f, joystickFadeDuration);
        joystickHandleImage.DOFade(1f, joystickFadeDuration);
    }
    private void HideJoystick(GameObject interactable)
    {
        joystickImage.DOFade(0f, joystickFadeDuration);
        joystickHandleImage.DOFade(0f, joystickFadeDuration).OnComplete(() => UIJoystick.SetActive(false));
    }

    public void PulseAnimation(Vector3 position)
    {
        pulse.transform.position = position;
        pulse.transform.DOScale(pulseMaxScale, pulseDuration);
        pulseImage.DOFade(0f, pulseDuration).OnComplete(ResetPulseAnimation);
    }
    private void ResetPulseAnimation()
    {
        pulseImage.color = new Color(1f, 1f, 1f, 1f);
        pulse.transform.localScale = Vector3.one;
        pulse.transform.localScale = new Vector2(0f, 0f);
    }


    public void TransitionIn()
    {
        transitionAnimation.gameObject.SetActive(true);
    }


    private void OnDestroy()
    {
        gameManager.onFreeMovementState -= ShowJoystick;
        gameManager.onFreeMovementState -= HideInteractionMenu;

        gameManager.onInteractingState -= HideJoystick;
        gameManager.onInteractingState -= ShowInteractionMenu;

        gameManager.onTransitionState -= TransitionIn;
    }
}
